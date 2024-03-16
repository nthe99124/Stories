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
            [Description("Tiền chuyển khoản, VNpay")]
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

        public enum GenderType : short
        {
            [Description("Nam")]
            Male,
            [Description("Nữ")]
            Female,
            [Description("Khác")]
            Other,
        }

        public enum TargetObject : short
        {
            [Description("Vị thành niên")]
            Juvenile,
            [Description("Nữ giới")]
            Female,
            [Description("Nam giới")]
            Male,
            [Description("Thiếu nhi")]
            Children,
        }
    }
}
