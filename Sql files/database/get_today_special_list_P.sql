CREATE DEFINER=`root`@`localhost` PROCEDURE `GET_TODAY_SPECIAL_LIST`()
BEGIN
SELECT P.recid,P.name,T.special_price,P.product_type,P.media_file,P.sku,
P.description,T.start_date,T.end_date from tbl_today_special T
JOIN tbl_product P ON P.recid=T.product_id AND P.status=1
WHERE T.status=1 AND current_date() between T.start_date AND T.end_date
ORDER BY T.recid DESC
LIMIT 3;
END