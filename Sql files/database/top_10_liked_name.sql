CREATE DEFINER=`root`@`localhost` PROCEDURE `TOP_10_LIKE_NAME`()
BEGIN
select N.name_ug `Name`,S.* from tbl_uyghur_name AS N JOIN(
select A.name_recid Recid, COUNT(CASE A.action WHEN 'LIKE' THEN 1 END) as `Like`
FROM tbl_name_action AS A 
#JOIN tbl_uyghur_name AS N ON A.name_recid=N.recid
 GROUP BY A.name_recid ORDER BY `Like`  DESC
LIMIT 10) as S ON N.recid=S.Recid;
END