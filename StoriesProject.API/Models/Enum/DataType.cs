using System.ComponentModel;

namespace StoriesProject.API.Model.Enum
{
    public class DataType
    {
        public enum CoinLogType : Int16
        {
            [Description("Nạp tiền")]
            Deposit,
            [Description("Rút tiền")]
            Withdrawal,
        }
        public enum OrderStatus : Int16
        {
            [Description("Chưa thanh toán")]
            NotPaidYet,
            [Description("Đã thanh toán, chờ duyệt")]
            PaidPendingApproval,
            [Description("Đã thanh toán, đã duyệt")]
            PaidApproval,
        }
        public enum OrderPaymentMethod : Int16
        {
            [Description("Tiền chuyển khoản, Momo")]
            Momo,
            [Description("Tiền chuyển khoản, Momo")]
            VNPay,
        }
        
        public enum TypeOfStory: Int16
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
