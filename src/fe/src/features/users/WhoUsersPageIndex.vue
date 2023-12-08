<template>
  <div>
    <UsersTable
      v-if="canRead"
      :can-delete="canDelete"
      :can-create="canCreate"
      :can-edit="canEdit"
      :users="users"
      :roles="roles"
      :loading="loading"
      :hide-is-active="true"
      :hide-operational-focal-point="true"
      :overwrite-headers="overwriteHeaders"
      title="WHO Users"
      @selected="selected"
      @create="create"
      @edit="editItem"
      @delete="deleteItem"
    >
    </UsersTable>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { AppModule } from "../../store/MainStore";
import UsersTable from "./components/UsersTable.vue";
import { UserModule } from "./store";
import { RoleModule } from "../roles/store";
import { User } from "@/models/User";
import { RoleType } from "@/models/enums/RoleType";
import { Role } from "@/models/Role";
import { AppError } from "@/models/shared/Error";
import { hasPermission } from "../../utils/helper";
import { PermissionNames } from "@/models/constants/PermissionNames";

@Component({ components: { UsersTable } })
export default class WhoUsersPageIndex extends Vue {
  get loading(): boolean {
    return AppModule.IsLoadingActive;
  }

  get canRead(): boolean {
    return hasPermission(PermissionNames.CanReadUser);
  }

  get canCreate(): boolean {
    return hasPermission(PermissionNames.CanCreateUser);
  }

  get canEdit(): boolean {
    return hasPermission(PermissionNames.CanEditUser);
  }

  get canDelete(): boolean {
    return hasPermission(PermissionNames.CanDeleteUser);
  }

  get detailRouting(): Map<string, string> {
    var dictionary = new Map<string, string>();
    dictionary.set("whoarea-users", "whoarea-user-details");
    // dictionary.set(
    //   "biohubfacilityarea-laboratories",
    //   "biohubfacilityarea-laboratory-details"
    // );

    return dictionary;
  }

  get users(): Array<User> {
    return UserModule.Users.filter((u) => {
      const roleType =
        RoleModule.Roles.find((r) => r.Id == u.RoleId)?.RoleType ??
        RoleType.Laboratory;
      return roleType == RoleType.WHO;
    });
  }

  get roles(): Array<Role> {
    return RoleModule.Roles.filter((r) => {
      return r.RoleType == RoleType.WHO;
    });
  }

  get overwriteHeaders(): Array<{
    newText: string;
    value: string;
    hide: boolean;
  }> {
    return [
      {
        newText: "Name",
        value: "Member",
        hide: false,
      },
      {
        newText: "",
        value: "OperationalFocalPoint",
        hide: true,
      },
      {
        newText: "",
        value: "JobTitle",
        hide: true,
      },
    ];
  }

  async loadPageInfo() {
    await RoleModule.ListRoles();
    await UserModule.ListUsers();
  }

  async mounted() {
    try {
      await this.loadPageInfo();
    } finally {
      AppModule.HideLoading();
    }
  }

  updated() {
    AppModule.HideLoading();
  }

  get error(): AppError | undefined {
    return UserModule.Error;
  }

  selected(item: User): void {
    UserModule.SET_USER(item);
    if (this.$route.name != null && this.$route.name != undefined) {
      var route = this.detailRouting.get(this.$route.name);
      if (route != null && route != undefined) {
        this.$router.push({
          name: route,
          params: { id: item.Id },
        });
      } else {
        this.$router.push({
          name: "whoarea-user-details",
          params: { id: item.Id },
        });
      }
    } else {
      this.$router.push({
        name: "whoarea-user-details",
        params: { id: item.Id },
      });
    }
  }

  create(): void {
    this.$router.push({
      name: "whoarea-user-create",
    });
  }

  editItem(item: User): void {
    UserModule.SET_USER(item);
    this.$router.push({
      name: "whoarea-user-edit",
      params: { id: item.Id },
    });
  }

  async deleteItem(item: User): Promise<void> {
    UserModule.SET_USER(item);
    await UserModule.DeleteUser();
    await UserModule.ListUsers();
    return;
  }
}
</script>
