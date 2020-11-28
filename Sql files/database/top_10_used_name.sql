CREATE DEFINER=`root`@`localhost` PROCEDURE `TOP_10_USED_NAME`()
BEGIN
select N.name_ug `Name`,S.* from tbl_uyghur_name AS N JOIN(
select A.name_recid Recid, 
COUNT(CASE A.action WHEN 'MYNAME' THEN 1 END) as MyName
FROM tbl_name_action AS A 

 GROUP BY A.name_recid ORDER BY MyName DESC
LIMIT 10) as S ON N.recid=S.Recid;
END