CREATE DEFINER=`root`@`localhost` PROCEDURE `DELETE_TIP`(IN id_in INT)
BEGIN
UPDATE tbl_tips SET `status`='DELETED'  WHERE recid=id_in;
END