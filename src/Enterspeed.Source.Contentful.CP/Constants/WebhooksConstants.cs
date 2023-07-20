namespace Enterspeed.Source.Contentful.CP.Constants;

public class WebhooksConstants
{
    public static string EventHeaderKey => "X-Contentful-Topic";
    
    public class Events
    {
        public static string EntryPublish => "ContentManagement.Entry.publish";
        public static string EntryUnpublish => "ContentManagement.Entry.unpublish";
        public static string EntryDelete => "ContentManagement.Entry.delete";
        public static string AssetPublish => "ContentManagement.Asset.publish";
        public static string AssetUnpublish => "ContentManagement.Asset.unpublish";
        public static string AssetDelete => "ContentManagement.Asset.delete";
        public static string Seed => "ContentManagement.Seed";
    }
    
    public class Types
    {
        public static string Entry => "Entry";
        public static string DeletedEntry => "DeletedEntry";
        public static string Asset => "Asset";
        public static string DeletedAsset => "DeletedAsset";
    }
}