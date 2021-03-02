using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces.IRepository;
using Domain.Entities.Core;

namespace Application.Infrastructure.Token
{
    /// <summary>
    /// This class is used to get the necessary token from the request and pass to the child class
    /// 
    /// Claess that wish to get this data has to inherit this class and
    /// </summary>
    public class TokenCredentials
    {
        private string _userId { get; set; }

        private string _userType { get; set; }

        private ICollection<string> _userRole { get; set; } 

        private IUnitOfWork _unitOfWork { get; set; }  

        /// <summary>
        /// User 
        /// </summary>
        /// <value></value>
        private User User { get; set; }

        /// <summary>
        /// Return the user Id
        /// </summary>
        /// <returns></returns>
        public string GetUserId() {
            return _userId;
        }

        /// <summary>
        /// Gets the type of user
        /// </summary>
        /// <returns></returns>
        public string GetUserType() {
            return _userType.ToLower();
        }        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="User"></param>
        /// <param name="unitOfWork"></param>
        public async Task setTokens(ClaimsPrincipal User, IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
            SetUserId(User);
            SetUserType(User);
            // await SetCurrentUser();
        }

        private void SetUserId(ClaimsPrincipal User)
        {
            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        private void SetUserType(ClaimsPrincipal User)
        {
            // _userType = User.FindFirst(AppClaimTypes.userType.ToString()).Value;
        }

        // Validate Tokens

        /// <summary>
        /// Checks if the user is the owner of the entity, intended for manipulation
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public void IsUserTheOwner(string userId) 
        {
            if (!userId.Equals(_userId))
                throw new CustomMessageException("Invalid access level");
        }

        // /// <summary>
        // /// Set the current user with the details
        // /// </summary>
        // /// <returns></returns>
        // public async Task SetCurrentUser() {
        //     if (GetUserType() == EntityDiscriminator.individual.ToString().ToLower())
        //         User = await SetIndividual();

        //     if (GetUserType() == EntityDiscriminator.organization.ToString().ToLower())
        //         User = await SetOrganization();

        //     if (GetUserType() == EntityDiscriminator.staff.ToString().ToLower())
        //         User = await SetStaff();

        //     if (User.LockoutEnd.HasValue && User.LockoutEnd.Value.UtcDateTime.ToUniversalTime() > DateTime.Now.ToUniversalTime())
        //         throw new CustomMessageException($"This account has been locked for now", HttpStatusCode.Unauthorized);
        // }

        // private async Task<Individual> SetIndividual() {
        //     Individual = await _unitOfWork.Individual.GetIndividual(GetUserId());
        //     if (Individual == null)
        //         throw new CustomMessageException("Individual not found", HttpStatusCode.NotFound);

        //     return Individual;
        // }

        // /// <summary>
        // /// Get the individual 
        // /// </summary>
        // /// <returns></returns>
        // public Individual GetIndividual() {
        //     return Individual;
        // }

        // private async Task<Organization> SetOrganization() {
        //     Organization = await _unitOfWork.Organization.GetOrganization(GetUserId());
        //     if (Organization == null)
        //         throw new CustomMessageException("Organization not found", HttpStatusCode.NotFound);

        //     return Organization;
        // }

        // /// <summary>
        // /// Return the organization
        // /// </summary>
        // /// <returns></returns>
        // public Organization GetOrganization() {
        //     return Organization;
        // }

        // private async Task<Staff> SetStaff() {
        //     Staff = await _unitOfWork.Admin.GetStaff(GetUserId());
        //     if (Staff == null)
        //         throw new CustomMessageException("Staff not found", HttpStatusCode.NotFound);

        //     return Staff;
        // }

        // /// <summary>
        // /// Get staffs
        // /// </summary>
        // /// <returns></returns>
        // public Staff GetStaff() {
        //     return Staff;
        // }

        // /// <summary>
        // /// Return the user of the platform
        // /// </summary>
        // /// <returns></returns>
        // public User GetUser() {
        //     return User;
        // }
    }
    
}