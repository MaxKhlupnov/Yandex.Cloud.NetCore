
module "dotnet-vpc" {
  source       = "./modules/vpc"
  network_name = var.yc_vpc_network_name
  subnets = {
    "dotnet-data-subnet" : {
      zone           = var.yc_main_zone
      v4_cidr_blocks = ["10.0.11.0/24"]
    }
  }
}


module "managed_pgsql_dotnet" {

  source       = "./modules/mdb-postgresql"
  cluster_name = "dotnet"
  network_id   =  module.dotnet-vpc.vpc_network_id
  description  = ".NetCore demo PostgreSQL database"
  labels = {
    env        = "dotnetcore",
    deployment = "terraform"
  }
  resource_preset_id = "b2.medium"
  disk_size          = 50

  hosts = [
    {
      zone             = var.yc_main_zone
      subnet_id        = module.dotnet-vpc.subnet_ids_by_names["dotnet-data-subnet"]
      assign_public_ip = true
    }
  ]
  users = [
    {
      name     = "auth_db_user"
      password = random_password.password.result
    }
  ]
  databases = [
    {
      name  = var.auth_db_name
      owner = "auth_db_user"
    }
  ]
  user_permissions = {
    "auth_db_user" : [
      {
        database_name = var.auth_db_name
      }
    ]}

}