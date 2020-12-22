CREATE DEFINER=`root`@`localhost` PROCEDURE `GET_TODAY_SPECIAL_LIST_ALL`()
BEGIN
SELECT P.recid,P.name,T.special_price,P.price,P.media_file,T.start_date,T.end_date from tbl_today_special T
JOIN tbl_product P ON P.recid=T.product_id AND P.status=1
ORDER BY T.recid DESC;
END