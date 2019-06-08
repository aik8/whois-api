import { TestingModule, Test } from '@nestjs/testing';
import { PiosService } from './pios.service';

describe('PiosService', () => {
	let piosService: PiosService;

	beforeEach(async () => {
		const app: TestingModule = await Test.createTestingModule({
			providers: [PiosService]
		}).compile();

		piosService = app.get<PiosService>(PiosService);
	});

	it('should be defined', () => {
		expect(piosService).toBeDefined();
	});

	it('should be an instance of PiosService', () => {
		expect(piosService).toBeInstanceOf(PiosService);
	});

	describe('parseResponse', () => {
		it('should return a JSON object from the HTML registry\'s response', () => {
			piosService.query('devoid.gr')
				.then(result => {
					expect(result).toHaveProperty('domain');
					expect(result).toHaveProperty('registrar');
					expect(result).toHaveProperty('nameServers');
					expect(result).toHaveProperty('registered');
				});
		});

		it('should anticipate a "not registered" response', () => {
			piosService.query('oaseofaisoevfiasovndasvasdvmdsa.gr')
				.then(result => {
					expect(result).toHaveProperty('domain');
					expect(result).toHaveProperty('registered');
				});
		});
	});
});
