using AutoMapper;
using JwtMusicProjectCase.Business.Dtos.AppUserDtos;
using JwtMusicProjectCase.Business.Dtos.ArtistDtos;
using JwtMusicProjectCase.Business.Dtos.GenreDtos;
using JwtMusicProjectCase.Business.Dtos.PackageDtos;
using JwtMusicProjectCase.Business.Dtos.PlaylistDtos;
using JwtMusicProjectCase.Business.Dtos.SongDtos;
using JwtMusicProjectCase.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.Business.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Genre, ResultGenreDto>().ReverseMap();
            CreateMap<Genre, CreateGenreDto>().ReverseMap();
            CreateMap<Genre, UpdateGenreDto>().ReverseMap();
            CreateMap<Genre, GetByIdGenreDto>().ReverseMap();

            CreateMap<Artist, CreateArtistDto>().ReverseMap();
            CreateMap<Artist, UpdateArtistDto>().ReverseMap();
            CreateMap<Artist, ResultArtistDto>().ReverseMap();
            CreateMap<Artist, GetByIdArtistDto>().ForMember(x => x.SongCount, y => y.MapFrom(src => src.Songs.Count))
                .ReverseMap();

            CreateMap<Package, CreatePackageDto>().ReverseMap();
            CreateMap<Package, UpdatePackageDto>().ReverseMap();
            CreateMap<Package, ResultPackageDto>().ReverseMap();
            CreateMap<Package, GetByIdPackageDto>().ReverseMap();

            CreateMap<Song, CreateSongDto>().ReverseMap();
            CreateMap<Song, UpdateSongDto>().ReverseMap();
            CreateMap<Song, ResultSongDto>()
                .ForMember(dest => dest.ArtistName, opt => opt.MapFrom(src => src.Artist.ArtistName))
                .ForMember(dest => dest.PackageName, opt => opt.MapFrom(src => src.Package.PackageName))
                .ForMember(dest => dest.PackageLevel, opt => opt.MapFrom(src => src.Package.PackageLevel))
                .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Genre.GenreName))
                .ReverseMap();
            CreateMap<Song, GetByIdSongDto>()
                .ForMember(dest => dest.ArtistName, opt => opt.MapFrom(src => src.Artist.ArtistName))
                .ForMember(dest => dest.ArtistImageUrl, opt => opt.MapFrom(src => src.Artist.ArtistImageUrl))
                .ForMember(dest => dest.PackageName, opt => opt.MapFrom(src => src.Package.PackageName))
                .ForMember(dest => dest.PackageLevel, opt => opt.MapFrom(src => src.Package.PackageLevel))
                .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Genre.GenreName))
                .ReverseMap();

            CreateMap<Playlist, CreatePlaylistDto>().ReverseMap();
            CreateMap<Playlist, UpdatePlaylistDto>().ReverseMap();
            CreateMap<Playlist, ResultPlaylistDto>().ReverseMap();
            CreateMap<Playlist, GetByIdPlaylistDto>().ReverseMap();

            CreateMap<AppUser, ResultUserDto>().ForMember(x => x.PackageName, opt => opt.MapFrom(src => src.Package.PackageName))
                .ReverseMap();
        }
    }
}
