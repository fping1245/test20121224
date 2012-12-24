resize();

// 重新調整母頁框的高度
function resize()
{
	var ifobj;
	ifobj = parent.document.getElementById("if_win");
	if (ifobj != null)
	{
		ifobj.style.height = (document.body.clientHeight) + "px";
	}

	ifobj = parent.document.getElementById("div_win");
	if (ifobj != null)
	{
		ifobj.style.height = (document.body.clientHeight) + "px";
	}
}