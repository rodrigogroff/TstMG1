using System;

namespace GameSystem
{
	public partial class GameMap : BaseWorld
	{
		public void CreateBgPanel(GameMapPanel _event)
		{
			int colspan = 1,
				rowspan = 1,
				width = 100,
				height = 100;

			string	strColspan = _event.MyValues["colspan"] as string,
					strRowspan = _event.MyValues["rowspan"] as string,
					strWidth = _event.MyValues["width"] as string,
					strHeight = _event.MyValues["height"] as string;

			if (!String.IsNullOrEmpty(strColspan)) colspan = I(strColspan);
			if (!String.IsNullOrEmpty(strRowspan)) rowspan = I(strRowspan);
			if (!String.IsNullOrEmpty(strWidth)) width = I(strWidth);
			if (!String.IsNullOrEmpty(strHeight)) height = I(strHeight);

			long startPos = _event.x_pos;

			for (int row = 0; row < rowspan; ++row)
			{
				for (int col = 0; col < colspan; ++col)
				{
					switch (Convert.ToInt32(_event.MySubType))
					{
						case 1: lstBgObjects.Add(new BgPanel(this, _event)); break;
						case 2: lstBgObjects.Add(new ParallaxPanel(this, _event)); break;
						case 3: lstBgObjects.Add(new BgBlock(this, _event)); break;
					}

					_event.x_pos += width;
				}

				_event.y_pos += height;
				_event.x_pos = startPos;
			}
		}
	}
}
