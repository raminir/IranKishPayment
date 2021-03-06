﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ShopWebForm.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">
    <meta name="description" content="iran kish">
    <meta name="keywords" content="iran kish">
    <meta name="author" content="iran kish">
    <meta name="apple-mobile-web-app-title" content="iran kish">
    <link rel="shortcut icon" href="">
    <link rel="apple-touch-icon-precomposed" href="">
    <title>ایران کیش</title>
    <link rel="stylesheet" href="Content/Layouts/irankish.fa-IR/build/css/bootstrap.css" />
    <link rel="stylesheet" href="Content/Layouts/irankish.fa-IR/build/css/bootstrap-rtl.min.css" />
    <link rel="stylesheet" href="Content/Layouts/irankish.fa-IR/build/css/bootstrapConfig.css" />
    <link rel="stylesheet" href="Content/Layouts/irankish.fa-IR/build/css/general-dashboard-page-style.css" />
    <link rel="stylesheet" href="Content/Layouts/irankish.fa-IR/build/css/general-nav-responsive.css" />
</head>
<body>
    <!--navbar-->
    <nav class="navbar navbar-default navbar-static-top" role="navigation">
        <div class="container p-r-xs-0 p-l-xs-0">
            <p class="navbar-text navbar-left hidden-xs">سامانه پرداخت اینترنتی ایران کیش</p>
            <div class="navbar-header">
                <a class="navbar-brand" href="#" style="float: none; margin: 0 auto; display: block">
                    <img src="Content/Layouts/irankish.fa-IR/imgs/irankish_logo_2X.png"
                        alt="شرکت کارت اعتباری ایران کیش" />

                </a>
            </div>

        </div>
    </nav>
    <!--/navbar-->
    <form id="form1" runat="server" class="container">
        <table class="table">
            <tr>
                <td>
                    <div class="row">
                        <asp:HyperLink NavigateUrl="~/Payment.aspx" Text="خرید عادی" runat="server" Font-Bold="true" Font-Size="XX-Large" CssClass="alert-link"></asp:HyperLink>

                    </div>

                </td>
            </tr>
            <tr>
                <td>
                    <div>

                        <asp:HyperLink runat="server" Text="خرید چند حسابی" Font-Bold="true" Font-Size="XX-Large" CssClass="alert-link navbar-link" NavigateUrl="MultiAccountPage.aspx" />


                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>

                        <asp:HyperLink runat="server" Text="پرداخت قبض" Font-Bold="true" Font-Size="XX-Large" CssClass="alert-link navbar-link" NavigateUrl="~/BillPayement.aspx" />


                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
