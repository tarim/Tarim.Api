CREATE DEFINER=`root`@`localhost` PROCEDURE `GET_NAME_STATISTICS`()
BEGIN
select COUNT(CASE gender WHEN 'male' THEN 1 END) as MaleCount,
COUNT(CASE gender WHEN 'female' THEN 1 END) as FemaleCount, 
COUNT(CASE gender WHEN 'unisex' THEN 1 END) as UnisexCount,
count(*) as Total,
GET_ACTION_COUNT('LIKE') as LikeCount,
GET_ACTION_COUNT('LOVE') as LoveCount,
GET_ACTION_COUNT('MYNAME') as MyNameCount
 from tbl_uyghur_name;

# select COUNT(CASE `action` WHEN 'LIKE' THEN 1 END) as LikeCount,
# COUNT(CASE `action` WHEN 'LOVE' THEN 1 END) as LoveCount,
# COUNT(CASE `action` WHEN 'MYNAME' THEN 1 END) as MyNameCount
# from tbl_name_action;
END