import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { Connection } from 'typeorm';

import { AppController } from './app.controller';
import { AppService } from './app.service';
import { NameServersModule } from './name-servers/name-servers.module';

@Module({
	imports: [
		TypeOrmModule.forRoot(),
		NameServersModule,
	],
	controllers: [AppController],
	providers: [AppService],
})
export class AppModule {
	constructor(private readonly connection: Connection) { }
}
