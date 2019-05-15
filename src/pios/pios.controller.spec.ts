import { Test, TestingModule } from '@nestjs/testing';
import { PiosController } from './pios.controller';
import { PiosService } from './pios.service';

describe('PiosController', () => {
	let piosController: PiosController;

	beforeEach(async () => {
		const app: TestingModule = await Test.createTestingModule({
			controllers: [PiosController],
			providers: [PiosService]
		}).compile();

		piosController = app.get<PiosController>(PiosController);
	});

	describe('basic query', () => {
		it('should return a result set', () => {
			expect(piosController.pios('devoid.gr')).toBeTruthy();
		});
	});
});
