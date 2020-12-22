CREATE DEFINER=`root`@`localhost` PROCEDURE `DELETE_PRODUCT`(IN id_in INT)
BEGIN
delete from tbl_product where recid=id_in;
END