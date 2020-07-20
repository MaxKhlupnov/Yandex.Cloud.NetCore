variable "yc_oauth_token" {
  description = "YC OAuth token"
  default     = ""
  type        = "string"
}

variable "yc_cloud_id" {
  description = "ID of a cloud"
  default     = ""
  type        = "string"
}

variable "yc_folder_id" {
  description = "ID of a folder"
  default     = ""
  type        = "string"
}

variable "yc_main_zone" {
  description = "The main availability zone"
  default     = "ru-central1-a"
  type        = "string"
}

variable "default_labels" {
  description = "Set of labels"
  default     = { "env" = "prod", "deployment" = "terraform" }
  type        = map(string)
}

variable "auth_db_name" {
  description = "Name postgre database for storing app & authentication data"
  default     = "iot_events"
  type        = "string"
}


variable "yc_vpc_network_name" {
  description = "Имя облачной сети для размещения инфраструктуры"
  default     = "data-svc-network"
  type        = "string"
}
