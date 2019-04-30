import { Sequelize } from 'sequelize-typescript';
import { Domain } from '../models/domains/domain.entity';
import { Snapshot } from 'src/models/snapshots/snapshot.entity';
import { Registrar } from 'src/models/registrars/registrar.entity';
import { NameServer } from 'src/models/ns/ns.entity';
import { SnapshotNameservers } from 'src/models/snapshots/snapshot-ns.entity';

export const databaseProviders = [
	{
		provide: 'SEQUELIZE',
		useFactory: async () => {
			const sequelize = new Sequelize('whois', 'whois', 'siohw', {
				dialect: 'mariadb'
			});

			sequelize.addModels([
				Domain,
				NameServer,
				Registrar,
				Snapshot,
				SnapshotNameservers
			]);

			Domain.hasMany(Snapshot, { as: 'snapshots' });
			Snapshot.hasOne(Registrar, { as: 'registrar' });

			Snapshot.belongsToMany(NameServer, { through: { model: SnapshotNameservers } });
			NameServer.belongsToMany(Snapshot, { through: SnapshotNameservers });

			await sequelize.sync();
			return sequelize;
		}
	}
];
