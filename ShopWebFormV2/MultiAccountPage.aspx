<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MultiAccountPage.aspx.cs" Inherits="ShopWebForm.MultiAccountPage" MasterPageFile="~/MyMasterPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="row">
            <div class="col-sm-6 col-sm-offset-3 m-t-xs-40">
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <asp:UpdatePanel ID="UP1" EnableViewState="true" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <table class="table-condensed ">
                                        <tr class="row ">
                                            <td>
                                                <label>مقدار-ریال</label>
                                            </td>
                                            <td class="col-md-10">
                                                <asp:TextBox ID="txtAmount" runat="server" class="form-control" BorderStyle="Groove" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr class=" row">
                                            <td>
                                                <label>کد پذیرنده</label>
                                            </td>
                                            <td class="col-md-10">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtAcceptorID" name="acceptorId" class="form-control" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr class="row">
                                            <td>
                                                <label>کد پایانه</label></td>
                                            <td>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtTerminalId" name="TerminalId" class="form-control" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                            <td></td>
                                        </tr>
                                         <tr class="row">
                                             <td> <label>عبارت عبور</label></td>
                                            <td>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtPassPhrase" name="PassPhrase" class="form-control" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <table class="table-condensed ">
                                        <tr class="row">
                                            <td class="label">
                                                <label>مبلغ 1- ریال</label>
                                            </td>
                                            <td class="col-md-12">
                                                <asp:TextBox ID="txtAmount1" runat="server" class="form-control" BorderStyle="Groove"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DropDownList0" runat="server"
                                                    OnSelectedIndexChanged="drpSheba1_TextChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label0" Text="" runat="server" ForeColor="Red" CssClass="label"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <table class="table-condensed ">
                                        <tr class="row ">
                                            <td class="label">
                                                <label>مبلغ 2- ریال</label>
                                            </td>
                                            <td class="col-md-10">
                                                <asp:TextBox ID="txtAmount2" runat="server" class="form-control" BorderStyle="Groove"></asp:TextBox></td>
                                            <td>
                                                <asp:DropDownList ID="DropDownList1" runat="server"
                                                    OnSelectedIndexChanged="drpSheba1_TextChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label1" Text="" runat="server" ForeColor="Red" CssClass="label"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <table class="table-condensed ">
                                <tr class="row ">
                                    <td>
                                        <label>توکن مشتری</label></td>
                                    <td class="col-md-10">
                                        <asp:TextBox ID="txtToken" class="form-control" runat="server" ReadOnly="true" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-10">
                        <div class="form-group">
                            <asp:Button ID="getToken" type="button" runat="server" class="btn btn-info ben-block form-control" Text="دریافت توکن" OnClick="getToken_Click"></asp:Button>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-10">
                        <asp:Literal ID="Literal1" runat="server" Text='</form><form name="subform" action="https://ikc.shaparak.ir/iuiv3/IPG/Index" method="POST" >'></asp:Literal>
                        <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                        <div class="form-group">
                            <button type="submit" class="btn btn-success btn-group-justified">پرداخت</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>