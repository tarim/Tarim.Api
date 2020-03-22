CREATE DEFINER=`root`@`localhost` PROCEDURE `DELETE_NAME`(IN id_in INT)
BEGIN
UPDATE tbl_uyghur_name SET `status`='DELETED'  WHERE recid=id_in;
END