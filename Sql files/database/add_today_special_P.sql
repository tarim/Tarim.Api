CREATE DEFINER=`root`@`localhost` PROCEDURE `ADD_TODAY_SPECIAL`(IN product_id_in INT,
 IN start_date_in TIMESTAMP,IN end_date_in TIMESTAMP,IN special_price_in VARCHAR(20)
 , OUT id_out INT)
BEGIN
INSERT INTO tbl_today_special(product_id,start_date,end_date,special_price) 
values(product_id_in,start_date_in,end_date_in,special_price_in);

SET id_out=(SELECT LAST_INSERT_ID());
END