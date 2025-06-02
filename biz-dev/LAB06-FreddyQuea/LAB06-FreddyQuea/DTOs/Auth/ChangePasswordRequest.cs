namespace LAB06_FreddyQuea.DTOs.Auth;

public class ChangePasswordRequest
{
    public string Username { get; set; }
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmNewPassword { get; set; }
}