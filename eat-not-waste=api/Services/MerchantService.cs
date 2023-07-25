using AutoMapper;
using eat_not_waste_api.Data;
using eat_not_waste_api.DTOs;
using eat_not_waste_api.Exceptions;
using eat_not_waste_api.Helpers;
using eat_not_waste_api.Models;
using eat_not_waste_api.Services;

public class MerchantService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly JwtAuthService _jwtAuthService;

    public MerchantService(
        ApplicationDbContext context, 
        IMapper mapper,
        JwtAuthService jwtAuthService
        )
    {
        _context = context;
        _mapper = mapper;
        _jwtAuthService = jwtAuthService;
    }

    public List<MerchantDto> GetAllMerchants()
    {
        var merchants = _context.Merchants.ToList();
        return _mapper.Map<List<MerchantDto>>(merchants);
    }

    public MerchantDto GetMerchantById(int id)
    {
        var merchant = _context.Merchants.Find(id);
        if (merchant == null)
        {
            return null;
        }
        return _mapper.Map<MerchantDto>(merchant);
    }

    // note - creating a merchant is handled in AuthenticationService.cs

    public MerchantDto UpdateMerchant(int id, MerchantDto merchantDto)
    {
        var merchant = _context.Merchants.Find(id);
        if (merchant == null)
        {
            return null;
        }

        _mapper.Map(merchantDto, merchant);
        _context.SaveChanges();

        return _mapper.Map<MerchantDto>(merchant);
    }

    public bool DeleteMerchant(int id)
    {
        var merchant = _context.Merchants.Find(id);
        if (merchant == null)
        {
            return false;
        }

        _context.Merchants.Remove(merchant);
        _context.SaveChanges();

        return true;
    }
}
