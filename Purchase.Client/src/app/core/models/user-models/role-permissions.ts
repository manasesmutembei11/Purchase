export interface Permission {
    name: string;
    description: string;
    canAccess: boolean;
}

export interface RoleModule {
    name: string;
    permissions: Permission[];
}

export interface RolePermissions {
    roleId: string;
    roleName: string;
    roleModules: RoleModule[];
}