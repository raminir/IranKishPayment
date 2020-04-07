<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerifyPage.aspx.cs" Inherits="ShopWebForm.VerifyPage" MasterPageFile="~/MyMasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <!--header-->
        <!--/header-->

        <div class="mainDiv">
            <div class="row m-t-xs-40 m-b-xs-20" data-ng-app="myApp" data-ng-controller="mainController">
                <div class="container">
                    <div class="row">
                        <div class="col-sm-6 col-sm-offset-3 m-t-xs-40">
                            <div class="form-group">
                                <div class="">
                                    <label id="message" style="font-size: xx-large" runat="server"></label>
                                </div>
                                <div class="">
                                    <label id="resp"   runat="server" ></label>
                                </div>
                                <div class="">
                                    <label id="lbltoken"   runat="server" visible="false"></label>
                                </div>
                                <div class="">
                                    <label id="lblParam" runat="server"></label>
                                </div>
                                 <div class="">
                                    <label id="lblAmount" runat="server" visible="false"></label>
                                </div>
                                <asp:TextBox ID="txtRRN" runat="server" class="form-control" BorderStyle="Groove"></asp:TextBox>
                                <asp:TextBox ID="txtTraceNo" runat="server" CssClass="form-control" BorderStyle="Groove"></asp:TextBox>
                                <asp:TextBox ID="txtAcceptorId" runat="server" class="form-control" BorderStyle="Groove"></asp:TextBox>
                                

                            </div>



                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Button ID="btnVerification" type="button" runat="server" class="btn btn-success ben-block form-control" Text="تایید تراکنش" OnClick="btnVerification_Click"></asp:Button>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Button ID="btnEspecialVerification" type="button" runat="server" class="btn btn-success ben-block form-control" Text="تایید تراکنش-با تاخیر" OnClick="btnEspecialVerification_Click"></asp:Button>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Button ID="btnReverce" type="button" runat="server" class="btn btn-info ben-block form-control" Text="برگشت تراکنش" OnClick="btnReverce_Click"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>





