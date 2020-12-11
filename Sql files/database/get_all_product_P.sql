CREATE DEFINER=`root`@`localhost` PROCEDURE `GET_ALL_PRODUCT`()
BEGIN
SELECT recid,name,price,product_type,media_file,sku,description,rec_create_datetime from tbl_product
 WHERE status=1;
END