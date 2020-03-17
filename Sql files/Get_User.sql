CREATE DEFINER=`root`@`localhost` PROCEDURE `GET_USER`(IN email_in VARCHAR(128))
BEGIN
SELECT recid,first_name,last_name,email,phone,role,description FROM tbl_user WHERE status=1 AND email=email_in;
END