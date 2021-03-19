using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeduCoreApp.Applications.Interfaces;
using TeduCoreApp.Applications.ViewModel.System;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.IRepositories;

namespace TeduCoreApp.Applications.Implementation
{
    public class PermissionService : IPermissionService
    {
        IPermissionRepository _permissionRepository;
        public PermissionService(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public bool AddPermission(List<PermissionViewModel> listPermissionVM)
        {
            foreach (var item in listPermissionVM)
            {
               
                var permission = Mapper.Map<PermissionViewModel, Permission>(item);
                _permissionRepository.Add(permission);
            }
            return true;
        }
        /// <summary>
        /// delete permission of role
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public bool DeletePermission(string roleId, List<PermissionViewModel> listPermissionVM)
        {
            List<Permission> permissions = _permissionRepository.FindAll(x => x.RoleId.ToString() == roleId).ToList();
            foreach(var permission in permissions)
            {
                _permissionRepository.Remove(permission);
            }
            foreach (var item in listPermissionVM)
            {

                var permission = Mapper.Map<PermissionViewModel, Permission>(item);
                _permissionRepository.Add(permission);
            }
            return true;
        }

        public List<PermissionViewModel> GetAllPermission()
        {
            var data = _permissionRepository.FindAll().ProjectTo<PermissionViewModel>().ToList();
            return data;
        }

        public List<PermissionViewModel> GetPermissonByRoleId(string roleId)
        {
            var data = _permissionRepository.FindAll(x=>x.RoleId.ToString() == roleId).ProjectTo<PermissionViewModel>().ToList();
            return data;

        }
    }
}
