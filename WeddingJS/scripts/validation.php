<?php
    $firstNameError1 = false;
    $firstNameError2 = false;
    $lastNameError1 = false;
    $lastNameError2 = false;
    $emailError1 = false;
    
    if(!empty($_POST['inputSubmitYes'])) {        
        $firstName = trim(filter_var($_POST['inputFirstName'], FILTER_SANITIZE_STRING));
        $lastName = trim(filter_var($_POST['inputLastName'], FILTER_SANITIZE_STRING));
        $email = trim(filter_var($_POST['inputEmail'], FILTER_SANITIZE_EMAIL));
	
        //validation
        if(strlen($firstName) == 0) {
            $firstNameError1 = true;            
        }
        
        if(!preg_match("/^[a-z0-9- ]+$/i",$firstName)) {
            $firstNameError2 = true;
        }
        
        if(strlen($lastName) == 0) {
            $lastNameError1 = true;
        }
        
        if(!preg_match("/^[a-z0-9- ]+$/i", $lastName)) {
            $lastNameError2 = true;
        }
	
	if(strlen($email) == 0) {
	    $emailError1 = true;
	}
        
        if(!$firstNameError1 && !$firstNameError2 && !$lastNameError1 && !$lastNameError2 && !$emailError1) {
            $rsvp = $_POST["RSVP"];
            $title = $_POST['inputTitle'];
            $numOfGuests = $_POST['inputNumOfGuests'];
                    
            if(strlen($_POST['inputGuestNames']) != 0) {               
                $guestNames = nl2br(htmlspecialchars($_POST['inputGuestNames']));
            }
            else {
                $guestNames = "None";
            }
            
            $entreeSelection = $_POST['inputEntree'];
            
            if(strlen($_POST['inputSpecialInstructions']) != 0) {           
                $specialInstructions = nl2br(htmlspecialchars($_POST['inputSpecialInstructions']));
            }
            else {
                $specialInstructions = "None";
            }
            
	    /*
            echo "Title: " . $title . "<br />";
            echo "First name: " . $firstName . "<br />";
            echo "Last name: " . $lastName . "<br />";
	    echo "Email: " . $email . "<br />";
            echo "RSVP: " . $rsvp . "<br />";
            echo "Number of Guests: " . $numOfGuests . "<br />";
            echo "Guest Names: " . $guestNames . "<br />";
            echo "Entree Selection: " . $entreeSelection . "<br />";
            echo "Special Instructions: " . $specialInstructions . "<br />";
            */
	    
	    //Send email, this will give a warning if still using on localhost. No problem when uploaded online
	    $to = $email;
	    $subject = "John and Shadi's Wedding RSVP - Yes";
	    $message = "Please do not reply to this email. \n\n
			Thank you, " . $firstName . " for RSVPing Yes. \n\n
			Attached is a copy of the email with information you submitted. \n\n"
			. "Title: " . $title . "\n"
			. "First name: " . $firstName . "\n"
			. "Last name: " . $lastName . "\n"
			. "Email: " . $email . "\n"
			. "RSVP: " . $rsvp . "\n"
			. "Number of Guests: " . $numOfGuests . "\n"
			. "Guest Names: " . $guestNames . "\n"
			. "Entree Selection: " . $entreeSelection . "\n"
			. "Special Instructions: " . $specialInstructions . "\n\n
			See you on the big day - June 30, 2017!";
			
	    $headers = "From: somebody@hotmail.com";
	    
	    if(mail($to, $subject, $message, $headers)) {
		header("Location: ../index.html?success=true");
		exit;
	    }
	    else {
		header("Location: ../index.html?success=true");
		exit;
	    }
        }
        else if($firstNameError1) {
            header("Location: ../index.html?error1=true");
            exit;
        }
	else if($firstNameError2) {
	    header("Location: ../index.html?error2=true");
	    exit;
	}
	else if($lastNameError1) {
	    header("Location: ../index.html?error3=true");
	    exit;
	}
	else if($lastNameError2) {
	    header("Location: ../index.html?error4=true");
	    exit;
	}
	else {
	    header("Location: ../index.html?error5=true");
	    exit;
	}
    }
    else {
        $firstName = trim(filter_var($_POST['inputFirstName'], FILTER_SANITIZE_STRING));
        $lastName = trim(filter_var($_POST['inputLastName'], FILTER_SANITIZE_STRING));
	$email = trim(filter_var($_POST['inputEmail'], FILTER_SANITIZE_EMAIL));
        
        //validation
        if(strlen($firstName) == 0) {
            $firstNameError1 = true;            
        }
        
        if(!preg_match("/^[a-z0-9- ]+$/i", $firstName)) {
            $firstNameError2 = true;
        }
        
        if(strlen($lastName) == 0) {
            $lastNameError1 = true;
        }
        
        if(!preg_match("/^[a-z0-9- ]+$/i", $lastName)) {
            $lastNameError2 = true;
        }
	
	if(strlen($email) == 0) {
	    $emailError1 = true;
	}
        
        if(!$firstNameError1 && !$firstNameError2 && !$lastNameError1 && !$lastNameError2 && !$emailError1) {
            $rsvp = $_POST["RSVP"];
            $title = $_POST['inputTitle'];
            
	    /*
            echo "Title: " . $title . "<br />";
            echo "First name: " . $firstName . "<br />";
            echo "Last name: " . $lastName . "<br />";
	    echo "Email: " . $email . "<br />";
            echo "RSVP: " . $rsvp . "<br />";*/
	    
	    //Send email, this will give a warning if still using on localhost. No problem when uploaded online
	    $to = $email;
	    $subject = "John and Shadi's Wedding RSVP - No";
	    $message = "Please do not reply to this email. \n\n
			Thank you, " . $firstName . " for RSVPing No. \n\n
			Attached is a copy of the email with information you submitted. \n\n"
			. "Title: " . $title . "\n"
			. "First name: " . $firstName . "\n"
			. "Last name: " . $lastName . "\n"
			. "Email: " . $email . "\n"
			. "RSVP: " . $rsvp . "\n\n
			Thank you taking the time to RSVP back to us!";
			
	    $headers = "From: somebody@hotmail.com";
	    $headers .= "BCC: somebody@hotmail.com";
	    
	    if(mail($to, $subject, $message, $headers)) {
		header("Location: ../index.html?success=true");
		exit;
	    }
	    else {
		header("Location: ../index.html?success=true");
		exit;
	    }
        }
        else if($firstNameError1) {
            header("Location: ../index.html?error1=true");
            exit;
        }
	else if($firstNameError2) {
	    header("Location: ../index.html?error2=true");
	    exit;
	}
	else if($lastNameError1) {
	    header("Location: ../index.html?error3=true");
	    exit;
	}
	else if($lastNameError2) {
	    header("Location: ../index.html?error4=true");
	    exit;
	}
	else {
	    header("Location: ../index.html?error5=true");
	    exit;
	}
    }

?>