<%@ Page Title="" Language="C#" MasterPageFile="~/MyMasterPage.Master" AutoEventWireup="true" CodeBehind="BillPayement.aspx.cs" Inherits="ShopWebForm.BillPayement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                                    <label>شناسه قبض</label>
                                    <asp:TextBox ID="txtBillId" runat="server" class="form-control" BorderStyle="Groove"></asp:TextBox>
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
                                    <label>شناسه پرداخت</label>
                                    <asp:TextBox ID="txtPaymentId" runat="server" class="form-control" BorderStyle="Groove"></asp:TextBox>
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
                                    <%-- <label>توکن مشتری</label>--%>
                                    <asp:TextBox ID="txtToken" class="form-control" runat="server" Enabled="false" Visible="false" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">

                                <div class="form-group">

                                    <asp:Button ID="getToken" type="button" runat="server" class="btn btn-info ben-block form-control" Text="پرداخت" OnClick="getToken_onclick"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </td>
                    <td></td>
                </tr>




            </table>
        </div>


    </div>


    <%-- <asp:Literal ID="Literal1" runat="server" Text='</form><form name="subform" action="https://ikc.shaparak.ir/TPayment/Payment/Index" method="POST" >'></asp:Literal>

    <asp:Literal ID="Literal2" runat="server"></asp:Literal>
    <asp:Literal ID="Literal3" runat="server"></asp:Literal>
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <div class="col-sm-6 col-sm-offset-3 m-t-xs-40">
                    <div class="form-group">
                        <button type="submit"  class="btn btn-success btn-group-justified">پرداخت</button>
                    </div>
                </div>
            </div>
        </div>

    </div>--%>
</asp:Content>
