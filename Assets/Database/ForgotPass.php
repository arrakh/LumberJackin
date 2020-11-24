<?php
    include_once("DBConnector.php");
    use PHPMailer\PHPMailer\Exception;
    use PHPMailer\PHPMailer\PHPMailer;
    
    require 'C:/xampp/htdocs/LumberJackin/PHPMailer/src/PHPMailer.php';
    require 'C:/xampp/htdocs/LumberJackin/PHPMailer/src/SMTP.php';
    require 'C:/xampp/htdocs/LumberJackin/PHPMailer/src/Exception.php';

    $user = $_POST['username'];
    $permitted_chars = '0123456789abcdefghijklmnopqrstuvwxyz';
    $pesan = substr(str_shuffle($permitted_chars), 0, 5);
    $sql = "SELECT email FROM login where username = '$user'";
    $query = mysqli_query($connection, $sql);
    $test = mysqli_fetch_assoc($query);
    
    $mail = new PHPMailer(TRUE);
    $mail->isSMTP();
    $mail->Host = 'smtp.gmail.com';
    $mail->Username = 'ilhamjp17@gmail.com';
    $mail->Password = 'ilhamjalu17'; 
    $mail->Port = 587;
    $mail->SMTPAuth = true;
    $mail->SMTPDebug = 1;

    $mail->setFrom('ilhamjp17@gmail.com', 'TEST');
    $mail->addAddress($test['email']);
    $mail->Subject = "VERIFICATION CODE";
    $mail->Body    = "MASUKKAN KODE INI : " . $pesan;
    $mail->send();

    if(!$mail->Send()) {
        echo "Mailer Error: " . $mail->ErrorInfo;
    } else {
        echo "Message has been sent ";
    }

    $input_data = "UPDATE login SET kode_verifikasi = '$pesan' WHERE username = '$user'";
    mysqli_query ($connection, $input_data);

?>