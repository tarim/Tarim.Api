CREATE DEFINER=`root`@`localhost` PROCEDURE `TOP_10_LOVED_NAME`()
BEGIN
select N.name_ug `Name`,S.* from tbl_uyghur_name AS N JOIN(
SELECT A.name_recid Recid,
COUNT(CASE A.action WHEN 'LOVE' THEN 1 END) as Love
FROM tbl_name_action AS A 
 GROUP BY A.name_recid ORDER BY  Love DESC
LIMIT 10) as S ON N.recid=S.Recid;
END