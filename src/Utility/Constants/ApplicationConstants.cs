using System.Runtime.CompilerServices;

namespace Utility.Constants
{
    public class ApplicationConstants
    {
        public const string KEYID_EXISTED = "KeyId {0} đã tồn tại.";
        public const string KeyId = "KeyId";
        public const string DUPLICATE = "Symtem_id is duplicated";
    }

    public class ResponseCodeConstants
    {
        public const string NOT_FOUND = "Not found!";
        public const string BAD_REQUEST = "Bad request!";
        public const string SUCCESS = "Success!";
        public const string FAILED = "Failed!";
        public const string EXISTED = "Existed!";
        public const string DUPLICATE = "Duplicate!";
        public const string INTERNAL_SERVER_ERROR = "INTERNAL_SERVER_ERROR";
        public const string INVALID_INPUT = "Invalid input!";
        public const string UNAUTHORIZED = "Unauthorized!";
        public const string FORBIDDEN = "Forbidden!";
        public const string EXPIRED = "Expired!";
    }
    public class ResponseMessageConstantsCommon
    {
        public const string NOT_FOUND = "Không tìm thấy dữ liệu";
        public const string EXISTED = "Already existed!";
        public const string SUCCESS = "Thao tác thành công";
        public const string NO_DATA = "Không có dữ liệu trả về";
        public const string SERVER_ERROR = "Lỗi từ phía server vui lòng liên hệ đội ngũ phát triển";
        public const string DATE_WRONG_FORMAT = "Dữ liệu ngày không đúng định dạng yyyy-mm-dd";
        public const string DATA_NOT_ENOUGH = "Dữ liệu đưa vào không đầy đủ";
    }

    public class ResponseMessageIdentity
    {
        public const string INVALID_USER = "Nguoi dung khong ton tai.";
        public const string UNAUTHENTICATED = "Khong xac thuc.";
        public const string PASSWORD_NOT_MATCH = "Mat khau khong giong nhau.";
        public const string PASSWORD_WRONG = "Mat khau khong dung.";
        public const string EXISTED_USER = "Nguoi dung da ton tai.";
        public const string EXISTED_EMAIL = "Email da ton tai.";
        public const string EXISTED_PHONE = "So dien thoai da ton tai.";
        public const string TOKEN_INVALID = "token khong xac thuc.";
        public const string TOKEN_EXPIRED = "token khong xac thuc hoac da het han.";
        public const string TOKEN_INVALID_OR_EXPIRED = "token khong xac thuc hoac da het han.";
        public const string GOOGLE_TOKEN_INVALID = "Invalid Google token.";
        public const string EMAIL_VALIDATED = "Email da duoc xac thuc.";
        public const string PHONE_VALIDATED = "Phone number is validated.";
        public const string ROLE_INVALID = "Roles khong xac thuc.";
        public const string CLAIM_NOTFOUND = "Khong tim thay claim.";
        public const string EXISTED_ROLE = "Role da ton tai.";

        public const string USERNAME_REQUIRED = "Ten nguoi dung khong duoc de trong.";
        public const string NAME_REQUIRED = "Ten khong duoc de trong.";
        public const string USERCODE_REQUIRED = "Ma nguoi dung khong duoc de trong.";
        public const string PASSWORD_REQUIRED = "Mat khau khong duoc de trong.";
        public const string PASSSWORD_LENGTH = "Mat khau phai co it nhat 5 ky tu.";
        public const string CONFIRM_PASSWORD_REQUIRED = "Xac nhan mat khau khong duoc de trong.";
        public const string EMAIL_REQUIRED = "Email khong duoc de trong.";
        public const string PHONENUMBER_REQUIRED = "So dien thoai khong duoc de trong.";
        public const string PHONENUMBER_INVALID = "So dien thoai khong hop le.";
        public const string PHONENUMBER_LENGTH = "So dien thoai phai co chinh xac 10 so.";
        public const string ROLES_REQUIRED = "Role khong duoc de trong.";
        public const string USER_NOT_ALLOWED = "Ban khong co quyen truy cap vao muc nay";
        public const string EMAIL_VALIDATION_REQUIRED = "Please enter the OTP code sent to your email to activate your account.";
    }

    public class ResponseMessageIdentitySuccess
    {
        public const string REGIST_USER_SUCCESS = "Dang ky tai khoan thanh cong! Vui long xac thuc email de kich hoat tai khoan";
        public const string VERIFY_PHONE_SUCCESS = "Xac thuc so dien thoai thanh cong!";
        public const string VERIFY_EMAIL_SUCCESS = "Xac thuc email thanh cong!";
        public const string FORGOT_PASSWORD_SUCCESS = "Yeu cau cap lai mat khau thanh cong, vui long kiem tra email";
        public const string RESET_PASSWORD_SUCCESS = "Cap lai mat khau thanh cong!";
        public const string CHANGE_PASSWORD_SUCCESS = "Doi mat khau thanh cong!";
        public const string RESEND_EMAIL_SUCCESS = "Gui lai email xac thuc thanh cong!";
        public const string UPDATE_USER_SUCCESS = "Cap nhat thong tin nguoi dung thanh cong!";
        public const string DELETE_USER_SUCCESS = "Xoa nguoi dung thanh cong!";
        public const string ADD_ROLE_SUCCESS = "Them role thanh cong!";
        public const string UPDATE_ROLE_SUCCESS = "Cap nhat role thanh cong!";
        public const string DELETE_ROLE_SUCCESS = "Xoa role thanh cong!";
    }

    // Response message constants for entities: not found, existed, update success, delete success
    public class ResponseMessageConstantsUser
    {
        public const string USER_NOT_FOUND = "Không tìm thấy người dùng";
        public const string USER_EXISTED = "Người dùng đã tồn tại";
        public const string ADD_USER_SUCCESS = "Thêm người dùng thành công";
        public const string UPDATE_USER_SUCCESS = "Cập nhật người dùng thành công";
        public const string DELETE_USER_SUCCESS = "Xóa người dùng thành công";
        public const string ADMIN_NOT_FOUND = "Không tìm thấy quản trị viên";
        public const string CUSTOMER_NOT_FOUND = "Không tìm thấy khách hàng";
    }
        
    public class ResponseMessageConstrantsShop
    {
        public const string IS_INACTIVE_OWNER = "Tài khoản của người dùng chưa được xác thực.";
        public const string EXISTED_EMAIL = "Email cua hang da ton tai.";
        public const string OWNER_NOTFOUND = "Tài khoản chủ cửa hàng không tìm thấy.";
        public const string EXISTED_NAME = "Ten cua da ton tai.";
        public const string EXISTED_ADDRESS = "Dia chi cua hang da ton tai.";
        public const string EXISTED_AVATAR = "Avatar da ton tai.";
        public const string NO_INISACTIVE_SHOP_FOUND = "Tất cả cửa hàng đã được duyệt.";
        public const string ALREADY_APROVED = "Cửa hàng đã được duyệt.";
        public const string EXISTED_PHONE = "So dien thoai cua hang da ton tai.";
        public const string ALREADY_OWNED_ANOTHER_SHOP = "Đã sở hữu một cửa hàng khác.";
        public const string PHONE_VALIDATED = "Phone number is validated.";
        public const string NOTFOUND = "Cửa hàng không tồn tại.";
    }

    public class ResponseMessageConstrantsProduct
    {
        public const string EXISTED_PRODUCTNAME = "Ten san pham da ton tai.";
        public const string NOTFOUND = "San pham khong ton tai.";
    }
}