using System.ComponentModel;

namespace StoriesProject.Model.Enum
{
    public class DataType
    {
        public enum CoinLogType : short
        {
            [Description("Nạp tiền")]
            Deposit,
            [Description("Rút tiền")]
            Withdrawal,
        }
        public enum OrderStatus : short
        {
            [Description("Chưa thanh toán")]
            NotPaidYet,
            [Description("Đã thanh toán, chờ duyệt")]
            PaidPendingApproval,
            [Description("Đã thanh toán, đã duyệt")]
            PaidApproval,
        }
        public enum OrderPaymentMethod : short
        {
            [Description("Tiền chuyển khoản, Momo")]
            Momo,
            [Description("Tiền chuyển khoản, Momo")]
            VNPay,
        }

        public enum TypeOfStory : short
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
