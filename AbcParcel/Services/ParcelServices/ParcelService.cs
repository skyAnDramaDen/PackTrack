using AbcParcel.Common.Contract;
using AbcParcel.Common;
using AbcParcel.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AbcParcel.Services.ParcelServices
{
    public interface IParcelService
    {
        Task<Response<string>> CreateListParcel(IEnumerable<CreateParcel> model);
        Task<Response<string>> CreateParcel(CreateParcel model);
        Task<Response<string>> CurrentLocation(string trackingNumber);
        Task<Response<string>> DeleteParcel(string trackingNumber);
        bool DeleteRecordbyId(long id);
        Task<Response<ParcelViewModel>> GetParcelById(string Id);
        Task<Response<ParcelViewModel>> GetParcelByTrackingNumber(string trackingNumber);
        Task<Response<IEnumerable<ParcelViewModel>>> GetParcelLists();
        Task<Response<string>> UpdateCurrentLocation(string Id, string location);
        Task<Response<string>> UpdateParcel(string trackingNumber, UpdateParcel model);
        Task<Response<string>> UpdateParcelView(UpdateParcelView model);
    }
    public class ParcelService : IParcelService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;
        public ParcelService(IMapper mapper, ApplicationDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<Response<ParcelViewModel>> GetParcelById(string Id)
        {
            var response = Response<ParcelViewModel>.Failed(string.Empty);
            try
            {
                var entity = await _dbContext.Parcels.FirstOrDefaultAsync(c => c.Id.ToString() == Id);
                if (entity == null)
                    return Response<ParcelViewModel>.Failed(ErrorMessages.ParcelNotFound);
                var mappedEntity = _mapper.Map<ParcelViewModel>(entity);
                response = Response<ParcelViewModel>.Success(mappedEntity);
            }
            catch (Exception ex)
            {
                response = Response<ParcelViewModel>.Failed(ex.Message);
            }
            return await Task.FromResult(response);
        }
        public async Task<Response<ParcelViewModel>> GetParcelByTrackingNumber(string trackingNumber)
        {
            var response = Response<ParcelViewModel>.Failed(string.Empty);
            try
            {
                var entity = await _dbContext.Parcels.FirstOrDefaultAsync(c => c.TrackingNumber == trackingNumber);
                if (entity == null)
                    return Response<ParcelViewModel>.Failed(ErrorMessages.ParcelNotFound);
                var mappedEntity = _mapper.Map<ParcelViewModel>(entity);
                response = Response<ParcelViewModel>.Success(mappedEntity);
            }
            catch (Exception ex)
            {
                response = Response<ParcelViewModel>.Failed(ex.Message);
            }
            return await Task.FromResult(response);
        }

        public async Task<Response<string>> CreateParcel(CreateParcel model)
        {
            var response = Response<string>.Failed(string.Empty);
            try
            {
                var trackingNumber = Helpers.GenerateTrackingNumber(model.OriginatingLocation);
                var existingEntity = await _dbContext.Parcels.FirstOrDefaultAsync(c => c.TrackingNumber == trackingNumber);

                if (existingEntity != null)
                    return Response<string>.Failed("Tracking number Already exists, please try again");

                Parcel modelParcel = new()
                {
                    DateCreated = DateTime.UtcNow,
                    Description = model.Description,
                    OriginatingLocation = model.OriginatingLocation,
                    FinalDestination = model.FinalDestination,
                    ParcelStatus = ParcelStatus.PickedUp,
                    TrackingNumber = trackingNumber,
                    CurrentLocation = model.OriginatingLocation
                };

                var addedEntity = await _dbContext.Parcels.AddAsync(modelParcel);
                await _dbContext.SaveChangesAsync();

                response = Response<string>.Success("Parcel Created Successfully");
            }
            catch (Exception ex)
            {
                response = Response<String>.Failed(ex.Message);
            }
            return await Task.FromResult(response);
        }

        public async Task<Response<string>> CreateListParcel(IEnumerable<CreateParcel> model)
        {
            var response = Response<string>.Failed(string.Empty);
            try
            {
                foreach (var item in model)
                {
                    var trackingNumber = Helpers.GenerateTrackingNumber(item.OriginatingLocation);
                    var existingEntity = await _dbContext.Parcels.FirstOrDefaultAsync(c => c.TrackingNumber == trackingNumber);
                    if (existingEntity != null)
                        return Response<string>.Failed("Tracking number Already exists, please try again");

                    existingEntity.DateCreated = DateTime.Now;
                    existingEntity.Description = item.Description;
                    existingEntity.OriginatingLocation = item.OriginatingLocation;
                    existingEntity.FinalDestination = item.FinalDestination;
                    var addedEntity = await _dbContext.AddAsync(existingEntity);
                }

                response = Response<string>.Success("Parcel Created Successfully");
            }
            catch (Exception ex)
            {
                response = Response<String>.Failed(ex.Message);
            }
            return await Task.FromResult(response);
        }

        public async Task<Response<string>> UpdateParcel(string trackingNumber, UpdateParcel model)
        {
            var response = Response<string>.Failed(string.Empty);
            try
            {
                var existingEntity = await _dbContext.Parcels.FirstOrDefaultAsync(c => c.TrackingNumber == trackingNumber);
                if (existingEntity == null)
                    return Response<string>.Failed("Parcel not found");

                existingEntity.Description = model.Description;
                existingEntity.FinalDestination = model.FinalDestination;

                _dbContext.Parcels.Update(existingEntity);
                await _dbContext.SaveChangesAsync();

                response = Response<string>.Success("Parcel Updated Successfully");
            }
            catch (Exception ex)
            {
                response = Response<string>.Failed(ex.Message);
            }
            return response;
        }

        public async Task<Response<string>> UpdateParcelView(UpdateParcelView model)
        {
            var response = Response<string>.Failed(string.Empty);
            try
            {
                var existingEntity = await _dbContext.Parcels.FirstOrDefaultAsync(c => c.Id == model.Id);

                if (existingEntity == null)
                    return Response<string>.Failed("Parcel not found");

                existingEntity.Description = model.Description;
                existingEntity.FinalDestination = model.FinalDestination;
                existingEntity.ParcelStatus = model.ParcelStatus;

                _dbContext.Parcels.Update(existingEntity);
                await _dbContext.SaveChangesAsync();

                response = Response<string>.Success("Location updated successfully");
            }
            catch (Exception ex)
            {
                response = Response<string>.Failed(ex.Message);
            }
            return await Task.FromResult(response);
        }
        public bool DeleteRecordbyId(long id)
        {
            var parcelToDelete = _dbContext.Parcels.Find(id);

            if (parcelToDelete != null)
            {
                _dbContext.Parcels.Remove(parcelToDelete);
                _dbContext.SaveChanges();
                return true;
            }

            return false;
        }
        public async Task<Response<string>> UpdateCurrentLocation(string Id, string location)
        {
            var response = Response<string>.Failed(string.Empty);
            try
            {
                var existingEntity = await _dbContext.Parcels.FirstOrDefaultAsync(c => c.Id.ToString() == Id);
                if (existingEntity == null)
                    return Response<string>.Failed("Parcel not found");

                existingEntity.CurrentLocation = location;

                _dbContext.Parcels.Update(existingEntity);
                await _dbContext.SaveChangesAsync();

                response = Response<string>.Success("Location updated successfully");
            }
            catch (Exception ex)
            {
                response = Response<string>.Failed(ex.Message);
            }
            return await Task.FromResult(response);
        }

        public async Task<Response<string>> DeleteParcel(string parcelId)
        {
            var response = Response<string>.Failed(string.Empty);
            try
            {
                var existingEntity = await _dbContext.Parcels.FirstOrDefaultAsync(c => c.Id.ToString() == parcelId);
                if (existingEntity == null)
                    return Response<string>.Failed("Parcel not found");

                _dbContext.Parcels.Remove(existingEntity);
                await _dbContext.SaveChangesAsync();

                response = Response<string>.Success("Parcel Deleted Successfully");
            }
            catch (Exception ex)
            {
                response = Response<string>.Failed(ex.Message);
            }
            return response;
        }
        public async Task<Response<string>> CurrentLocation(string trackingNumber)
        {
            var response = Response<string>.Failed(string.Empty);
            try
            {
                var entity = await _dbContext.Parcels.FirstOrDefaultAsync(c => c.TrackingNumber == trackingNumber);
                if (entity == null)
                    return Response<string>.Failed($"Parcel with tracking number: {trackingNumber} not found");
                var currentLocation = entity.CurrentLocation;

                response = Response<string>.Success(currentLocation);
            }
            catch (Exception ex)
            {
                response = Response<string>.Failed(ex.Message);
            }
            return await Task.FromResult(response);
        }

        public async Task<Response<IEnumerable<ParcelViewModel>>> GetParcelLists()
        {
            var response = Response<IEnumerable<ParcelViewModel>>.Failed(string.Empty);
            try
            {
                var entities = await _dbContext.Parcels.ToListAsync();

                var mappedEntities = _mapper.Map<IEnumerable<ParcelViewModel>>(entities);

                response = Response<IEnumerable<ParcelViewModel>>.Success(mappedEntities);
            }
            catch (Exception ex)
            {
                response = Response<IEnumerable<ParcelViewModel>>.Failed(ex.Message);
            }
            return await Task.FromResult(response);
        }
    }
}
