<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="ShopWebForm._Default" MasterPageFile="~/MyMasterPage.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <!--header-->
        <!--/header-->
        <div class="row">
            <table class="col-sm-6 col-sm-offset-3 m-t-xs-40">
                <tr>
                    <td>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>مقدار-ریال</label>
                                    <asp:TextBox ID="txtAmount" runat="server" class="form-control" BorderStyle="Groove"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>کد پایانه</label>
                                    <asp:TextBox ID="txtTerminalId" name="TerminalId" class="form-control" runat="server" />
                                </div>
                            </div>
                        </div>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">

                                    <label>کد پذیرنده</label>
                                    <asp:TextBox ID="txtAcceptorID" name="acceptorId" class="form-control" runat="server" />
                                </div>
                            </div>
                        </div>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">

                                    <label>عبارت عبور</label>
                                    <asp:TextBox ID="txtPassPhrase" name="PassPhrase" class="form-control" runat="server" />
                                </div>
                            </div>
                        </div>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>توکن مشتری</label>
                                    <asp:TextBox ID="txtToken" class="form-control" runat="server" ReadOnly="true" />
                                </div>
                            </div>
                        </div>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <div class="row">
                            <div class="col-md-12">

                                <div class="form-group">

                                    <asp:Button ID="getToken" type="button" runat="server" class="btn btn-info ben-block form-control" Text="دریافت توکن" OnClick="getToken_onclick"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:Literal ID="Literal1" runat="server" Text='</form><form name="subform" action="https://ikc.shaparak.ir/iuiv3/IPG/Index" method="POST" >'></asp:Literal>
                        <asp:Literal ID="Literal2" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <button type="submit" class="btn btn-success btn-group-justified">پرداخت</button>
                                </div>
                            </div>
                        </div>
                    </td>
                    <td></td>
                </tr>
            </table>
        </div>
</asp:Content>





