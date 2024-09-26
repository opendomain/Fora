﻿using AutoMapper;
using Fora.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fora.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Company : ControllerBase
    {
        private readonly IMapper _mapper;
        private IForaService _foraService;

        public Company(IMapper mapper, IForaService foraService)
        {
            _mapper = mapper;
            _foraService = foraService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Model.EdgarCompanyInfo>? allEdgarCompanies = _foraService.GetAllCompanies();

            List<Model.Company> allCompanies;

            allCompanies = _mapper.Map<List<Model.Company>>(allEdgarCompanies);

            return Ok(allCompanies);
        }

        [HttpGet("{Cik}")]
        public IActionResult Get(int Cik)
        {
            Model.EdgarCompanyInfo edgarCompany = _foraService.GetCompany(Cik);

            Model.Company company;
            company = _mapper.Map<Model.Company>(edgarCompany);

            return Ok(company);
        }

    }
}