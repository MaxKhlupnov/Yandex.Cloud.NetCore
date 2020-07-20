provider "yandex" {
  version   = "~> 0.29"
  token     = var.yc_oauth_token
  cloud_id  = var.yc_cloud_id
  folder_id = var.yc_folder_id
  zone      = var.yc_main_zone
}


resource "random_password" "password" {
  length = 16
  special = true
  min_special = 1
  upper = true
  min_upper = 1
  lower = true
  min_lower = 1
  number = true
  min_numeric = 1
  override_special = "_%@"
}

output "managed_pgsql_dotnet_cluster_fqdns" {
  value = module.managed_pgsql_dotnet.cluster_hosts_fqdns
}

output "managed_pgsql_dotnet_cluster_users" {
  value = module.managed_pgsql_dotnet.cluster_users
}

output "managed_pgsql_dotnet_cluster_users_passwords" {
  value     = module.managed_pgsql_dotnet.cluster_users_passwords
  sensitive = false
}

output "managed_pgsql_dotnet_cluster_databases" {
  value = module.managed_pgsql_dotnet.cluster_databases
}
