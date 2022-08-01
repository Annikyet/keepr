CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

SELECT * FROM keepr.accounts;

CREATE TABLE IF NOT EXISTS vaultKeeps(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT COMMENT 'primary key',
  creatorId VARCHAR(255) NOT NULL COMMENT 'id of vault creator',
  vaultId INT NOT NULL COMMENT 'ID of vault this keep is in',
  keepId INT NOT NULL COMMENT 'ID of keep'
) default charset utf8;

CREATE TABLE IF NOT EXISTS vaults(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT COMMENT 'primary key',
  creatorId VARCHAR(255) NOT NULL COMMENT 'id of vault creator',
  name VARCHAR(255) NOT NULL COMMENT 'name of vault',
  description VARCHAR(255) COMMENT 'description of vault'
  idPrivate TINYINT DEFAULT 0 COMMENT 'flag for if vault is private'
) default charset utf8;

CREATE TABLE IF NOT EXISTS keeps(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT COMMENT 'primary key',
  creatorId VARCHAR(255) NOT NULL COMMENT 'id of keep creator',
  name VARCHAR(255) NOT NULL COMMENT 'name of keep',
  description VARCHAR(255) COMMENT 'description of keep',
  img VARCHAR(255) DEFAULT 'assets/img/404.svg' COMMENT 'URL of image',
  views INT DEFAULT 0 COMMENT 'Number of views',
  kept INT DEFAULT 0 COMMENT 'Number of times added to a vault'
) default charset utf8;


      SELECT *
      FROM accounts
      WHERE id = '62df16f445a8ad25b2c9485c';

              SELECT *
        FROM vaults
        WHERE creatorId = '62df16f445a8ad25b2c9485c'
        AND isPrivate = 0;

        select * from keeps;
        select * from accounts;

                SELECT
          a.*,
          k.*
        FROM keeps k
        JOIN accounts a
          ON a.id = k.creatorId;