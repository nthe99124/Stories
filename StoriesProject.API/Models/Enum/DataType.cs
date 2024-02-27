using System.ComponentModel;

namespace StoriesProject.API.Model.Enum
{
    public class DataType
    {
        public enum CoinLogType
        {
            [Description("Nạp tiền")]
            Deposit,
            [Description("Rút tiền")]
            Withdrawal,
        }
        public enum OrderStatus
        {
            [Description("Chưa thanh toán")]
            NotPaidYet,
            [Description("Đã thanh toán, chờ duyệt")]
            PaidPendingApproval,
            [Description("Đã thanh toán, đã duyệt")]
            PaidApproval,
        }
        public enum OrderPaymentMethod
        {
            [Description("Tiền chuyển khoản, Momo")]
            Momo,
            [Description("Tiền chuyển khoản, Momo")]
            VNPay,
        }
        
        public enum TypeOfStory
        {
            [Description("Truyện chữ")]
            Text,
            [Description("Truyện tranh")]
            Comic,
            [Description("Truyện chữ kèm tranh")]
            TextComic,
        }
    }
}
