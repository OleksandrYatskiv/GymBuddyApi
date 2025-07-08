namespace GymBuddyApi.Helpers;

public static class EmailText
{
    public const string subject = "Welcome to GymBuddy! Please confirm your email";

    public static string GetEmailText(string confirmationLink)
    {
        return $@"
<html>
<body style='font-family: Arial, sans-serif; line-height: 1.6; color: #333; max-width: 600px; margin: 0 auto; padding: 20px;'>
    <div style='background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); padding: 30px; text-align: center; border-radius: 10px 10px 0 0;'>
        <h1 style='color: white; margin: 0; font-size: 28px;'>🏋️‍♂️ Welcome to GymBuddy!</h1>
        <p style='color: white; margin: 10px 0 0 0; font-size: 16px;'>Your fitness journey starts here</p>
    </div>
    
    <div style='background: white; padding: 30px; border-radius: 0 0 10px 10px; box-shadow: 0 4px 10px rgba(0,0,0,0.1);'>
        <h2 style='color: #333; margin-top: 0;'>Hi there, future fitness champion! 💪</h2>
        
        <p>We're thrilled to have you join the GymBuddy community! You're just one step away from unlocking your personalized workout experience.</p>
        
        <p>To get started and secure your account, please confirm your email address by clicking the button below:</p>
        
        <div style='text-align: center; margin: 30px 0;'>
            <a href='{confirmationLink}' style='background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); color: white; padding: 15px 30px; text-decoration: none; border-radius: 25px; font-weight: bold; font-size: 16px; display: inline-block; transition: transform 0.2s;'>
                ✅ Confirm Email Address
            </a>
        </div>
        
        <p style='color: #666; font-size: 14px; margin-top: 30px;'>
            <strong>Can't click the button?</strong> Copy and paste this link into your browser:<br>
            <a href='{confirmationLink}' style='color: #667eea; word-break: break-all;'>{confirmationLink}</a>
        </p>
        
        <div style='border-top: 2px solid #f0f0f0; margin-top: 30px; padding-top: 20px;'>
            <p style='color: #666; font-size: 14px; margin: 0;'>
                🎯 <strong>What's next?</strong> Once confirmed, you'll be able to:<br>
                • Create personalized workout plans<br>
                • Track your progress<br>
                • Connect with your gym buddies<br>
                • Access exclusive fitness content
            </p>
        </div>
    </div>
    
    <div style='text-align: center; margin-top: 20px; color: #666; font-size: 12px;'>
        <p>This email was sent to you because you signed up for GymBuddy.<br>
        If you didn't sign up, please ignore this email.</p>
    </div>
</body>
</html>";
    }
}