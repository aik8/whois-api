import { Module } from '@nestjs/common';
import { NameServerController } from './ns.controller';
import { NameServersService } from './ns.service';
import { nsProviders } from './ns.provider';
import { DatabaseModule } from 'src/database/database.module';

@Module({
	imports: [DatabaseModule],
	controllers: [NameServerController],
	providers: [NameServersService, ...nsProviders]
})
export class NameServersModule { }
