CREATE DEFINER=`root`@`localhost` PROCEDURE `ADD_PRODUCT`(IN name_in VARCHAR(128),
 IN product_type_in VARCHAR(64),IN price_in VARCHAR(20),IN sku_in VARCHAR(20),IN media_file_in VARCHAR(512),
 IN description_in TEXT, OUT id_out INT)
BEGIN
INSERT INTO tbl_product(`name`,sku,product_type,description,price,media_file) 
values(name_in,sku_in,product_type_in,description_in,price_in,media_file_in);

SET id_out=(SELECT LAST_INSERT_ID());
END