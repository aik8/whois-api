import { MigrationInterface, QueryRunner } from 'typeorm';

export class RemoveTheLastEagerness1575933764262 implements MigrationInterface {
	name = 'RemoveTheLastEagerness1575933764262';

	public async up(queryRunner: QueryRunner): Promise<any> {
		await queryRunner.query('ALTER TABLE `registrar` CHANGE `url` `url` varchar(255) NULL', undefined);
		await queryRunner.query('ALTER TABLE `registrar` CHANGE `email` `email` varchar(255) NULL', undefined);
		await queryRunner.query('ALTER TABLE `registrar` CHANGE `phone` `phone` varchar(255) NULL', undefined);
		await queryRunner.query('ALTER TABLE `snapshot` DROP FOREIGN KEY `FK_6b9ac5b386fbe941c5422cce6f4`', undefined);
		await queryRunner.query('ALTER TABLE `snapshot` DROP FOREIGN KEY `FK_43f09af0274abf14e2a3b0d5103`', undefined);
		await queryRunner.query('ALTER TABLE `snapshot` CHANGE `registrarId` `registrarId` int NULL', undefined);
		await queryRunner.query('ALTER TABLE `snapshot` CHANGE `registrarName` `registrarName` varchar(255) NULL', undefined);
		await queryRunner.query('ALTER TABLE `snapshot` CHANGE `domainId` `domainId` int NULL', undefined);
		await queryRunner.query('ALTER TABLE `snapshot` CHANGE `domainName` `domainName` varchar(255) NULL', undefined);
		await queryRunner.query('ALTER TABLE `domain` CHANGE `handle` `handle` varchar(255) NULL', undefined);
		await queryRunner.query('ALTER TABLE `domain` CHANGE `protonum` `protonum` varchar(255) NULL', undefined);
		await queryRunner.query('ALTER TABLE `domain` CHANGE `creation` `creation` datetime NULL', undefined);
		await queryRunner.query('ALTER TABLE `domain` CHANGE `expiration` `expiration` datetime NULL', undefined);
		await queryRunner.query('ALTER TABLE `domain` CHANGE `last_update` `last_update` datetime NULL', undefined);
		await queryRunner.query('ALTER TABLE `snapshot` ADD CONSTRAINT `FK_6b9ac5b386fbe941c5422cce6f4` FOREIGN KEY (`registrarId`, `registrarName`) REFERENCES `registrar`(`id`,`name`) ON DELETE NO ACTION ON UPDATE NO ACTION', undefined);
		await queryRunner.query('ALTER TABLE `snapshot` ADD CONSTRAINT `FK_43f09af0274abf14e2a3b0d5103` FOREIGN KEY (`domainId`, `domainName`) REFERENCES `domain`(`id`,`name`) ON DELETE NO ACTION ON UPDATE NO ACTION', undefined);
	}

	public async down(queryRunner: QueryRunner): Promise<any> {
		await queryRunner.query('ALTER TABLE `snapshot` DROP FOREIGN KEY `FK_43f09af0274abf14e2a3b0d5103`', undefined);
		await queryRunner.query('ALTER TABLE `snapshot` DROP FOREIGN KEY `FK_6b9ac5b386fbe941c5422cce6f4`', undefined);
		await queryRunner.query("ALTER TABLE `domain` CHANGE `last_update` `last_update` datetime NULL DEFAULT 'NULL'", undefined);
		await queryRunner.query("ALTER TABLE `domain` CHANGE `expiration` `expiration` datetime NULL DEFAULT 'NULL'", undefined);
		await queryRunner.query("ALTER TABLE `domain` CHANGE `creation` `creation` datetime NULL DEFAULT 'NULL'", undefined);
		await queryRunner.query("ALTER TABLE `domain` CHANGE `protonum` `protonum` varchar(255) NULL DEFAULT 'NULL'", undefined);
		await queryRunner.query("ALTER TABLE `domain` CHANGE `handle` `handle` varchar(255) NULL DEFAULT 'NULL'", undefined);
		await queryRunner.query("ALTER TABLE `snapshot` CHANGE `domainName` `domainName` varchar(255) NULL DEFAULT 'NULL'", undefined);
		await queryRunner.query("ALTER TABLE `snapshot` CHANGE `domainId` `domainId` int NULL DEFAULT 'NULL'", undefined);
		await queryRunner.query("ALTER TABLE `snapshot` CHANGE `registrarName` `registrarName` varchar(255) NULL DEFAULT 'NULL'", undefined);
		await queryRunner.query("ALTER TABLE `snapshot` CHANGE `registrarId` `registrarId` int NULL DEFAULT 'NULL'", undefined);
		await queryRunner.query('ALTER TABLE `snapshot` ADD CONSTRAINT `FK_43f09af0274abf14e2a3b0d5103` FOREIGN KEY (`domainId`, `domainName`) REFERENCES `domain`(`id`,`name`) ON DELETE NO ACTION ON UPDATE NO ACTION', undefined);
		await queryRunner.query('ALTER TABLE `snapshot` ADD CONSTRAINT `FK_6b9ac5b386fbe941c5422cce6f4` FOREIGN KEY (`registrarId`, `registrarName`) REFERENCES `registrar`(`id`,`name`) ON DELETE NO ACTION ON UPDATE NO ACTION', undefined);
		await queryRunner.query("ALTER TABLE `registrar` CHANGE `phone` `phone` varchar(255) NULL DEFAULT 'NULL'", undefined);
		await queryRunner.query("ALTER TABLE `registrar` CHANGE `email` `email` varchar(255) NULL DEFAULT 'NULL'", undefined);
		await queryRunner.query("ALTER TABLE `registrar` CHANGE `url` `url` varchar(255) NULL DEFAULT 'NULL'", undefined);
	}

}
