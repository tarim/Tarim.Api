CREATE DEFINER=`root`@`localhost` PROCEDURE `ADD_TIP`(IN title_in VARCHAR(128),IN summary_in VARCHAR(256),
IN category_in ENUM('General', 'DevOps', 'Go', 'CSharap', 'Api', 'React', 'CSS', 'Oracle', 'MySql', 'RFM'),
 IN content_in text,IN source_in VARCHAR(128),IN user_recid_in INT, OUT id_out INT)
BEGIN
INSERT INTO tbl_tips(title,summary,category,content,`source`,user_recid) 
values(title_in,summary_in,category_in,content_in,source_in,user_recid_in);

SET id_out=(SELECT LAST_INSERT_ID());
END