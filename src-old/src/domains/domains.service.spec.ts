import { Test, TestingModule } from '@nestjs/testing';
import { TypeOrmModule } from '@nestjs/typeorm';
import { Domain, NameServer, Registrar, Snapshot } from '../models';
import { DomainsService } from './domains.service';

const base = {
	name: 'devoid.gr',
	handle: 'd154348bce4794113acd4e07cada7a7df-gr',
	protonum: '1623093'
};

const sample1 = Object.assign({}, base, { expiration: new Date('2019-11-20T00:00') });
const sample2 = Object.assign({}, base, { expiration: new Date('2019-12-30T00:00') });

describe('DomainsService', () => {
	let domainsService: DomainsService;

	beforeAll(async () => {
		const app: TestingModule = await Test.createTestingModule({
			imports: [
				TypeOrmModule.forRoot({
					type: process.env.TYPEORM_CONNECTION as any || 'mariadb',
					host: process.env.TYPEORM_HOST || 'localhost',
					username: process.env.TYPEORM_USERNAME || 'kootoor',
					password: process.env.TYPEORM_PASSWORD || 'funkybudha',
					database: process.env.TYPEORM_DATABASE || 'whois',
					port: Number.parseInt(process.env.TYPEORM_PORT || '3306', 10),
					entities: [Domain, NameServer, Registrar, Snapshot],
					migrations: ['../migrations/*.ts'],
					synchronize: false,
					logging: false
				}),
				TypeOrmModule.forFeature([Domain])
			],
			controllers: [],
			providers: [DomainsService]
		}).compile();

		domainsService = app.get<DomainsService>(DomainsService);
	});

	describe('findOrInsert', () => {
		it('should insert a domain that does not exist', done => {
			domainsService.findOrInsert(sample2)
				.then(domain => {
					expect(domain).toBeDefined();
					expect(domain).toMatchObject(base);
					done();
				});
		});

		it('should not try to re-insert a domain that already exists', done => {
			domainsService.findOrInsert(sample1)
				.then(domain => {
					expect(domain).toBeDefined();
					expect(domain).toMatchObject(base);
					done();
				});
		});
	});
});
