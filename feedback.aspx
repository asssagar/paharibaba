<%@ Page Language="VB" AutoEventWireup="false" CodeFile="feedback.aspx.vb" Inherits="feedback" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
		<!-- 
        Awesome Template
        http://www.templatemo.com/preview/templatemo_450_awesome
        -->
		<title>feedback</title>
		<meta name="keywords" content="">
		<meta name="description" content="">
		<meta http-equiv="X-UA-Compatible"content="IE=Edge">
		<meta name="viewport" content="width=device-width, initial-scale=1">
		
		<%--<link rel="stylesheet" href="css/search_scc.css">--%>
		<link rel="stylesheet" href="css/animate.min.css">
		<link rel="stylesheet" href="css/bootstrap.min.css">
		<link rel="stylesheet" href="css/font-awesome.min.css">
		<link href='http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700' rel='stylesheet' type='text/css'>
		<link rel="stylesheet" href="css/templatemo-style.css">
		<script src="js/jquery.js"></script>
		<script src="js/bootstrap.min.js"></script>
        <script src="js/jquery.singlePageNav.min.js"></script>
		<script src="js/typed.js"></script>
		<script src="js/wow.min.js"></script>
		<script src="js/custom.js"></script>
	<style>
#btn_log
{
width:45%;
height:40px;
background-color:#002878;
color:#fff;
}
#btn_log:hover
{
background-color:#d5bf67;
color:#002878;

}
 </style> 
  <style>
  	html,body{height:100%; margin:0px;}	
  </style>
</head>
<body style="background-image:url(images/background.jpg)">
    <form id="form1" runat="server">
    <div class="preloader">
			<div class="sk-spinner sk-spinner-wave">
     	 		<div class="sk-rect1"></div>
       			<div class="sk-rect2"></div>
       			<div class="sk-rect3"></div>
      	 		<div class="sk-rect4"></div>
      			<div class="sk-rect5"></div>
     		</div>
    	</div>
    	<!-- end preloader -->

        <!-- start header -->
        <header style="background-image:url(images/background.jpg)">
            <div class="container">
                <div class="row">
                    <div class="col-md-3 col-sm-4 col-xs-12">
                        <p style="float:left;"><i class="fa fa-phone"></i><span> Phone</span>+91-9431104181</p>
                        <p style="float:left;"><i class="fa fa-envelope-o"></i><span> Email</span><a href="#">ranchipmvs@gmail.com</a></p>
                    </div>
                    <div class="col-md-5 col-sm-4 col-xs-12">
                       <!-- <p><i class="fa fa-envelope-o"></i><span> Email</span><a href="#">ranchipmvs@gmail.com</a></p>-->
 <%--   						<form class="form-wrapper cf">
	<input type="text" placeholder="Search here..." required >
	<button type="submit" >Search</button>
</form>--%>
                    </div>
                    <div class="col-md-3 col-sm-4 col-xs-12">
                        <ul class="social-icon">
                            <li><span>Meet us on</span></li>
                            <li><a href="http://www.facebook.com/paharibabaranchi" class="fa fa-facebook"></a></li>
                            <li><a href="#" class="fa fa-twitter"></a></li>
                            <li><a href="#" class="fa fa-instagram"></a></li>
                            <li><a href="#" class="fa fa-apple"></a></li>
                        </ul>
<div align="center" style="margin-top:1%;">
<button type="button"   id="btn_log"><i class="fa fa-sign-in fa-lg" style="color:#fff;"></i> sing-in</button>
<button type="button"  id="Button1"><i class="fa fa-arrow-circle-right fa-lg" style="color:#fff;"></i> sing-up</button></div>
                    </div>
                </div>
            </div>
        </header>
        <!-- end header -->

    	<!-- start navigation -->
		<nav class="navbar navbar-default templatemo-nav" role="navigation">
			<div class="container">
				<div class="navbar-header">
					<button class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
						<span class="icon icon-bar"></span>
						<span class="icon icon-bar"></span>
						<span class="icon icon-bar"></span>
					</button>
					<a href="#" class="navbar-brand"><img src="images/logo.png" style="height: 60px;margin-top:-9px;width: 200px;"></img></a>
				</div>
				<div class="collapse navbar-collapse">
					<ul class="nav navbar-nav navbar-right">
						<li><a href="#top">HOME</a></li>
						<li><a href="#about">ABOUT</a></li>
						<li><a href="#portfolio">GALLERY</a></li>
						<li><a href="#service">FEEDBACK</a></li>
						<li><a href="#team">TEAM</a></li>
						<li><a href="#contact">DOWNLOAD</a></li>
						<li><a href="#portfolio">NEWS</a></li>
						<li><a href="#contact">FORUM</a></li>
					</ul>
				</div>
			</div>
		</nav>
		<!-- end navigation -->
  	<section id="contact" style="background-image:url(images/background.jpg);">
    		<div class="container">
    			<div class="row">
    				<div class="col-md-12">
    					<h2 class="wowbounceIn" data-wow-offset="50" data-wow-delay="0.3s">CONTACT<span> US</span></h2>
    				</div>
    				<div class="col-md-6 col-sm-6 col-xs-12 wow fadeInLeft" data-wow-offset="50" data-wow-delay="0.9s">
    					<%--<form action="#" method="post">--%>
    						<label style="float:left;">NAME</label>
    						<%--<input name="fullname" type="text" class="form-control" id="fullname">--%>
                            <asp:TextBox ID="txt_name" runat="server" CssClass="form-control"></asp:TextBox>
   						  	
                            <label style="float:left;">EMAIL</label>
    						<%--<input name="email" type="email" class="form-control" id="email">--%>
                            <asp:TextBox ID="txt_email" runat="server" CssClass="form-control"></asp:TextBox>

                                <label style="float:left;">Phone</label>
    						<%--<input name="email" type="email" class="form-control" id="email">--%>
                            <asp:TextBox ID="txt_phone" runat="server" CssClass="form-control"></asp:TextBox>
   						  	
                            <label style="float:left;">MESSAGE</label>
    						<%--<textarea name="message" rows="4" class="form-control" id="message"></textarea>--%>
                            <asp:TextBox ID="txt_msg" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>
    						
                            <%--<input type="submit" class="form-control">--%>
                           <asp:Button ID="btn_submit" runat="server" Text="submit"  class="form-control"/>
    				<%--	</form>--%>
    				</div>
    				<div class="col-md-6 col-sm-6 col-xs-12 wow fadeInRight" data-wow-offset="50" data-wow-delay="0.6s">
    					<address>
    						<p class="address-title">OUR ADDRESS</p>
    						<span>Lorem ipsum dolor sit amet, consectetur adipiscing elitquisque tempus ac eget diam et laoreet phasellus ut nisi id leo molestie.</span>
    						<p><i class="fa fa-phone"></i> 010-020-0340</p>
    						<p><i class="fa fa-envelope-o"></i> awesome@company.com</p>
    						<p><i class="fa fa-map-marker"></i> 663 New Walk Roadside, Birdeye View, GO 11020</p>
    					</address>
    					<ul class="social-icon">
    						<li><h4>WE ARE SOCIAL</h4></li>
    						<li><a href="#" class="fa fa-facebook"></a></li>
    						<li><a href="#" class="fa fa-twitter"></a></li>
    						<li><a href="#" class="fa fa-instagram"></a></li>
    					</ul>
    				</div>
    			</div>
    		</div>
    	</section>
    	<!-- end contact -->
<footer id="copyright" style="background-image:url(images/background.jpg);">
            <div class="container">
                <div class="row">
                    <div class="col-md-12 text-center">
                        <p class="wow bounceIn" data-wow-offset="50" data-wow-delay="0.3s">
                       	Copyright &copy; 2084 Company Name</p>
                    </div>
                </div>
            </div>
        </footer>
    </form>
</body>
</html>
