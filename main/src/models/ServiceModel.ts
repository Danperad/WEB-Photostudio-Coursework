declare type ServiceModel = {
	id: number,
	title: string,
	description: string,
	cost: number,
	serviceType: number,
	photos: string[],
	rating: number
}

export default ServiceModel;