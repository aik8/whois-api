export interface AppConfiguration {
	serverPort: number;
	redis?: {
		host: string,
		port: number,
		enabled: boolean | string
	};
	database: {
		host: string,
		port: number,
		schema: string,
		user: string,
		password: string
	};
}

export const config: AppConfiguration = {
	serverPort: Number(process.env.SERVER_PORT) || 3000,
	database: {
		host: process.env.DB_HOST || 'localhost',
		port: Number(process.env.DB_PORT) || 3306,
		schema: process.env.DB_SCHEMA || 'whois',
		user: process.env.DB_USER || 'whois',
		password: process.env.DB_PASS || 'siohw'
	}
};
