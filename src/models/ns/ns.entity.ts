import { Table, Model, Column } from 'sequelize-typescript';

@Table({
	timestamps: true,
	tableName: 'nses'
})
export class NameServer extends Model<NameServer> {
	@Column
	name: string;
}
