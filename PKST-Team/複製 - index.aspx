<%@ Page Title="" Language="C#" MasterPageFile="~/thismaster_樹狀(不使用).master" AutoEventWireup="true" CodeFile="複製 - index.aspx.cs" Inherits="index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


           
    <div id="contents0" class="clearfix">
    <div></div>
        <div class="topics">
					<!-- ▼主題標題 -->
					<h3>
                        中心公告</h3>
            <div>
                公告1 XXXXXXXX 公告1 XXXXXXX<br />
                公告2 XXXXXXXXXXXXXXX<br />
                公告3 XXXXXXXXXXXXXXX<br />
                公告4 XXXXXXXXXXXXXXX</div>
        </div>
        <div id="information">
					<!-- ▼網站公告標題 -->
					<h3>
                        中心會議紀錄</h3>
            <dl>
						<!-- ▼▼網站公告從此開始▼▼ -->
						<dt>2011.09.21</dt>
                <dd>
                    <a href=#>0921會議記錄。</a></dd>
                <dt>2011.09.18</dt>
                <dd>
                    <a href=#>0918會議記錄。</a></dd>
                <dt>2011.09.15</dt>
                <dd>
                    <a href=#>0915會議記錄。</a></dd>
                <dt>2011.08.28</dt>
                <dd>
                    <a href=#>0828會議記錄。</a></dd>
                <dt>2011.08.12</dt>
                <dd>
                    <a href=#>0812會議記錄。</a></dd>
						<!-- ▲▲網站公告到此為止▼▼ -->
					</dd>
            </dl>
        </div>
        <div class="topics">
					<!-- ▼主題標題 -->
					<h3>
                        技術專欄</h3>
            <ul>
						<!-- ▼▼主題1從此開始▼▼ -->
						<li>
                            <p class="photo">
                                <a href="service1.html">
                                <img alt="ITソリューション" height="90" src="img/topics_01.jpg" width="90" /></a></p>
                            <h4>
                                圖表的資料繫結</h4>
                            <p class="text">
                                在此介紹圖表的資料繫結。</p>
                            <p class="more">
                                <a href=#>詳情請點選此處</a></p>
						<!-- ▲▲主題1到此為止▼▼ -->
						<!-- ▼▼主題2從此開始▼▼ -->
						</li>
                <li>
                    <p class="photo">
                        <a href="service2.html">
                        <img alt="IT服務" height="90" src="img/topics_02.jpg" width="90" /></a></p>
                    <h4>
                        WCF服務</h4>
                    <p class="text">
                        在此介紹WCF服務。</p>
                    <p class="more">
                        <a href=#>詳情請點選此處</a></p>
						<!-- ▲▲主題2到此為止▼▼ -->
						<!-- ▼▼主題3從此開始▼▼ -->
						</li>
                <li>
                    <p class="photo">
                        <a href="faq.html">
                        <img alt="常見問題" height="90" src="img/topics_03.jpg" width="90" /></a></p>
                    <h4>
                        網頁製作常見問題</h4>
                    <p class="text">
                        在此列出網頁製作時常見的問題提供參考。</p>
                    <p class="more">
                        <a href=#>詳情請點選此處</a></p>
						<!-- ▲▲主題3到此為止▼▼ -->
						<!-- ▼▼主題4從此開始▼▼ -->
						</li>
                <li>
                    <p class="photo">
                        <a href="company.html">
                        <img alt="公司簡介" height="90" src="img/topics_04.jpg" width="90" /></a></p>
                    <h4>
                        專題實作</h4>
                    <p class="text">
                        在此為您介紹專題實作的特殊技巧。</p>
                    <p class="more">
                        <a href=#>詳情請點選此處</a></p>
						<!-- ▲▲主題4到此為止▼▼ -->
					</li>
            </ul>
        </div>
    </div>


           
</asp:Content>

