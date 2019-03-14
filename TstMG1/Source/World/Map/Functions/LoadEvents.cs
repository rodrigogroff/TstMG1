using System;
using System.IO;
using System.Linq;
using System.Globalization;

namespace GameSystem
{
	public partial class GameMap : BaseWorld
	{
		public void LoadMap()
		{
			LoadEvents(MyMapName);

			StartMap();
		}

		public void StartMap()
		{
			var ev = lstMapEvents.Where(y => y.MyType == "start").FirstOrDefault();

			ExecuteEvent(ev);
			lstMapEvents.Remove(ev);
		}

		public void LoadEvents(string strMapName)
		{
			string path = Directory.GetCurrentDirectory() + "\\Content\\Maps\\" + strMapName,
					content = "";

			using (var fileStream = new StreamReader(path))
				content = fileStream.ReadToEnd().Replace("\r\n", "\n").
												 Replace(";", "\n").
												 Replace("{", "{ \n").
												 Replace("}", "\n}");

			var arContent = content.Split('\n');

			NumberFormatInfo nfi = CultureInfo.InvariantCulture.NumberFormat;

			for (int t = 0; t < arContent.Length; ++t)
			{
				var line = arContent[t].Trim();
				if (line == "") continue;

				switch (line[0])
				{
					// comment
					case '#': continue;

					// path list

					// events
					case '%':
						{
							++t; // {
							++t; // next line

							var ev = new GameMapEvent();

							do
							{
								line = arContent[t].Trim();

								if (line == "") continue;
								if (line.StartsWith("}")) break;
								if (line.StartsWith("#")) continue;
								if (!line.Contains(":")) continue;

								var arLine = line.Split(':');
								var fieldName = arLine[0].ToLower().Trim();
								var fieldValue = arLine[1].Trim();

								switch (fieldName)
								{
									case "type": ev.MyType = fieldValue; break;
									case "frame": ev.MyFrame = Convert.ToInt64(fieldValue); break;
									case "p1_x_pos": ev.p1_x_pos = int.Parse(fieldValue, nfi); break;
									case "p1_y_pos": ev.p1_y_pos = int.Parse(fieldValue, nfi); break;

									case "p2_x_pos": ev.p2_x_pos = int.Parse(fieldValue, nfi); break;
									case "p2_y_pos": ev.p2_y_pos = int.Parse(fieldValue, nfi); break;

									case "world_x_speed": ev.world_x_speed = float.Parse(fieldValue, nfi); break;
									case "world_y_speed": ev.world_y_speed = float.Parse(fieldValue, nfi); break;
								}

								++t;
							}
							while (t < arContent.Length - 1);

							lstMapEvents.Add(ev);

							break;
						}
					
					// enemies, bosses
					case '$':
						{
							++t; // {
							++t; // next line

							var ev = new GameMapEnemy();

							do
							{
								line = arContent[t].Trim();

								if (line == "") continue;
								if (line.StartsWith("}")) break;
								if (line.StartsWith("#")) continue;
								if (!line.Contains(":")) continue;

								var arLine = line.Split(':');
								var fieldName = arLine[0].ToLower().Trim();
								var fieldValue = arLine[1].Trim();

								switch (fieldName)
								{
									case "label": ev.MyLabel = fieldValue; break;
									case "sub": ev.MySubType = fieldValue; break;
									case "image": ev.MyStrImage = fieldValue; break;
									case "x_pos": ev.x_pos = Convert.ToInt64(fieldValue); break;
									case "y_pos": ev.y_pos = Convert.ToInt64(fieldValue); break;

									default:
										ev.MyParams.Add(fieldName);
										ev.MyValues[fieldName] = fieldValue;
										break;
								}

								++t;
							}
							while (t < arContent.Length - 1);

							CreateEnemy(ev);

							break;
						}

					// backgrounds
					case '@':
						{
							++t; // {
							++t; // next line

							var ev = new GameMapPanel();

							do
							{
								line = arContent[t].Trim();

								if (line == "") continue;
								if (line.StartsWith("}")) break;
								if (line.StartsWith("#")) continue;
								if (!line.Contains(":")) continue;

								var arLine = line.Split(':');
								var fieldName = arLine[0].ToLower().Trim();
								var fieldValue = arLine[1].Trim();

								switch (fieldName)
								{
									case "sub": ev.MySubType = fieldValue; break;
									case "image": ev.MyStrImage = fieldValue; break;
									case "x_pos": ev.x_pos = Convert.ToInt64(fieldValue); break;
									case "y_pos": ev.y_pos = Convert.ToInt64(fieldValue); break;

									default:
										ev.MyParams.Add(fieldName);
										ev.MyValues[fieldName] = fieldValue;
										break;
								}

								++t;
							}
							while (t < arContent.Length - 1);

							CreateBgPanel(ev);

							break;
						}

					default: break;
				}
			}
		}
	}
}
