CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture'
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS vaults(
        id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
        name VARCHAR(255) NOT NULL COMMENT 'vault name',
        description VARCHAR(255) NOT NULL COMMENT 'vault body',
        isPrivate BOOLEAN NOT NULL COMMENT 'vault private',
        creatorId VARCHAR(255) NOT NULL COMMENT 'creator id',
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS keeps (
        id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
        name VARCHAR(255) NOT NULL,
        description VARCHAR(255) NOT NULL,
        img VARCHAR(255) NOT NULL,
        views INT,
        kept INT,
        creatorId VARCHAR(255) NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS vaultkeeps(
        id INT NOT NULL AUTO_INCREMENT primary key,
        keepId INT NOT NULl,
        vaultId INT NOT NULL,
        creatorId VARCHAR(255) NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
        FOREIGN KEY (keepId) REFERENCES keeps(id) ON DELETE CASCADE,
        FOREIGN KEY (vaultId) REFERENCES vaults(id) ON DELETE CASCADE
    ) default charset utf8;