namespace ShoeService_Api.Notifications
{
    public class CustomerNotification : NotificationBase
    {
        public const string Login_Success = "Đăng nhập thành công";
        public const string Login_Fail = "Đăng nhập thất bại";
        public const string Login_Null = "Thông tin đăng nhập không được để trống";
        public const string Login_Customer_NotActive = "Tài khoản của bạn đang bị khóa";

        public const string Register_Success = "Đăng ký thành công";
        public const string Register_Fail = "Đăng ký thất bại";
        public const string Register_Data_Null = "Thông tin đăng ký không được để trống";
        public const string Register_Exist_Email = "Email đã được sử dụng";
    }
}
