CREATE TABLE `tbl_users` (
  `rec_id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `first_name` varchar(64) COLLATE utf8_unicode_ci NOT NULL,
  `last_name` varchar(64) COLLATE utf8_unicode_ci NOT NULL,
  `email` varchar(128) COLLATE utf8_unicode_ci NOT NULL,
  `phone` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `role` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `description` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `rec_create_datetime` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `status` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`rec_id`),
  UNIQUE KEY `email_UNIQUE` (`email`)
) ENGINE=MyISAM AUTO_INCREMENT=1 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

INSERT INTO `tbl_users` VALUES (1,'Nur','Karluk','ns.karluk@gmail.com','5713099110','Admin',NULL,'2018-11-11 17:53:00',1),
(2,'Maynur','Karluk','maynur@gmail.com','5713099113','Admin',NULL,'2018-11-11 17:55:00',1);

CREATE TABLE `tbluyghur_name` (
  `uyghur_name_recid` int(16) NOT NULL AUTO_INCREMENT,
  `rec_create_datetime` datetime DEFAULT NULL,
  `rec_mod_datetime` timestamp NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
  `name` varchar(128) DEFAULT NULL,
  `category` enum('oghul','qiz','ortaq') DEFAULT 'oghul',
  `origination` enum('uyghurche','erepche','parische','bashqa') DEFAULT 'bashqa',
  `description` varchar(255) DEFAULT NULL,
  `status` enum('ACTIVE','DISABLED','DELETED') DEFAULT 'ACTIVE',
  `related_name` varchar(255) DEFAULT NULL,
  `is_surname` tinyint(1) DEFAULT '0',
  `come_from` varchar(64) DEFAULT NULL,
  PRIMARY KEY (`uyghur_name_recid`),
  UNIQUE KEY `name` (`name`)
) ENGINE=MyISAM AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `tarim_db`.`tbl_tips` (
  `recid` INT NOT NULL AUTO_INCREMENT,
  `rec_create_datetim` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `rec_mod_datetime` TIMESTAMP NOT NULL ON UPDATE CURRENT_TIMESTAMP,
  `title` VARCHAR(128) NOT NULL,
  `summary` VARCHAR(256) NULL,
  `category` VARCHAR(64) NOT NULL,
  `detail` TEXT NULL,
  `private` TINYINT NOT NULL DEFAULT 1,
  `source` VARCHAR(128) NULL,
  `user_recid` INT NOT NULL,
  PRIMARY KEY (`recid`),
  INDEX `private` (`private`) VISIBLE,
  INDEX `user_recid` (`user_recid`) VISIBLE)
ENGINE = InnoDB;

CREATE TABLE `tbl_uyghur_proverb` (
  `recid` int NOT NULL AUTO_INCREMENT,
  `rec_create_datetim` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `rec_mod_datetime` timestamp NOT NULL ON UPDATE CURRENT_TIMESTAMP,
  `proverb` varchar(512) COLLATE utf8_unicode_ci DEFAULT NULL,
  `user_recid` int NOT NULL,
  `status` varchar(20) COLLATE utf8_unicode_ci NOT NULL DEFAULT 'Active',
  `updated_by` int DEFAULT NULL,
  PRIMARY KEY (`recid`),
  KEY `added_user` (`user_recid`),
  KEY `display_status` (`status`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

CREATE TABLE `tbl_product` (
  `recid` INT NOT NULL AUTO_INCREMENT,
  `rec_create_datetim` TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP,
  `rec_mod_datetime` TIMESTAMP NULL ON UPDATE CURRENT_TIMESTAMP,
  `name` VARCHAR(128) NOT NULL,
  `sku` VARCHAR(20) NULL,
  `product_type` VARCHAR(64) NOT NULL DEFAULT 'Web',
  `description` TEXT NULL,
  `price` DECIMAL(10) NOT NULL DEFAULT 1,
  `media_url` VARCHAR(128) NULL,
  `user_recid` INT NOT NULL,
  PRIMARY KEY (`recid`));
  
  CREATE TABLE `tbl_today_special` (
  `recid` int NOT NULL DEFAULT '0',
  `rec_create_datetime` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `rec_mod_datetime` timestamp NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
  `product_id` int NOT NULL,
  `start_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `end_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `description` text COLLATE utf8_unicode_ci,
  `special_price` decimal(10,0) NOT NULL DEFAULT '1',
  `user_recid` int NOT NULL,
  `status` varchar(10) COLLATE utf8_unicode_ci DEFAULT 'Active',
  PRIMARY KEY (`recid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;